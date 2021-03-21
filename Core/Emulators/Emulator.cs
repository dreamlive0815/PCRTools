﻿using System;
using System.Drawing;
using System.Diagnostics;
using System.Text.RegularExpressions;

using Core.Common;
using Core.Extensions;

namespace Core.Emulators
{
    public abstract class Emulator
    {

        public Emulator()
        {
            //ConnectToAdbServer();
        }

        public abstract string Name { get; }

        public virtual bool Alive
        {
            get
            {
                var proc = GetMainProcess();
                return proc != null;
            }
        }

        public virtual Rectangle Area
        {
            get
            {
                var handle = GetMainWindowHandle();
                return GetAreaWithWindowHandle(handle);
            }
        }

        public virtual int AdbPort { get; } = 5555;

        public abstract Process GetMainProcess();

        public abstract IntPtr GetMainWindowHandle();

        public abstract string GetAdbExePath();

        protected string AheadWithName(string str)
        {
            return $"[{Name}]{str}";
        }

        protected void AssertAlive()
        {
            if (!Alive)
                throw new Exception(AheadWithName("无法检测到模拟器"));
        }

        protected Rectangle GetAreaWithWindowHandle(IntPtr handle)
        {
            var rect = Win32API.GetWindowRect(handle);
            if (!rect.Valid || rect.Width < 10 || rect.Height < 10)
                throw new Exception(AheadWithName("模拟器尺寸不合法"));
            return rect.ToRectangle();
        }

        protected Process GetProcessByName(string processName)
        {
            return ProcessExtension.GetProcessByName(processName);
        }

        protected Process FindProcessByName(string name)
        {
            var processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                var procName = process.ProcessName;
                if (Regex.IsMatch(procName, name))
                {
                    return process;
                }
            }
            return null;
        }

        protected Bitmap CaptureScreen(Rectangle rect)
        {
            return Utils.CaptureScreen(rect);
        }

        public Bitmap GetScreenCapture()
        {
            var rect = Area;
            return CaptureScreen(rect);
        }

        private void FastAdbCmd(string arguments)
        {
            ShellUtils.DoShell(new ShellArgs()
            {
                FileName = GetAdbExePath(),
                Arguments = arguments,
                IgnoreOutput = true,
            });
        }

        private string AdbCmd(string arguments)
        {
            var result = ShellUtils.DoShell(new ShellArgs()
            {
                FileName = GetAdbExePath(),
                Arguments = arguments,
                IgnoreOutput = false,
            });
            var output = result.GetOutput();
            Logger.GetInstance().Info("AdbCmd", AheadWithName($"{arguments}->{output.LimitLength(32)}"));
            return output;
        }

        private string AdbShell(string args)
        {
            var fullArguments = $"-s 127.0.0.1:{AdbPort} shell {args}";
            var output = AdbCmd(fullArguments);
            return output;
        }

        public void ConnectToAdbServer()
        {
            AdbCmd($"connect 127.0.0.1:{AdbPort}");
        }

        private EmulatorSize resolution;

        public EmulatorSize GetResolution()
        {
            if (!resolution.IsEmpty)
                return resolution;
            var output = AdbShell("wm size");
            var match = Regex.Match(output, "(\\d+)x(\\d+)");
            if (!match.Success)
                throw new Exception(AheadWithName("获取模拟器分辨率失败"));
            var size = new EmulatorSize(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            resolution = size;
            return size;
        }

        public void DoTap(PVec2f pf)
        {
            DoTap(GetResolution() * pf);
        }

        public void DoTap(EmulatorPoint point)
        {
            var output = AdbShell($"input tap {point.X} {point.Y}");
        }
    }

    public struct EmulatorPoint
    {
        public int X { get; set; }

        public int Y { get; set; }

        public EmulatorPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public struct EmulatorSize
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public bool IsEmpty { get { return Width == 0 && Height == 0; } }

        public EmulatorSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static EmulatorPoint operator *(EmulatorSize size, PVec2f pf)
        {
            var x = (int)(size.Width * pf.X);
            var y = (int)(size.Height * pf.Y);
            return new EmulatorPoint(x, y);
        }
    }

    public struct PVec2f
    {
        public float X;
        public float Y;

        public PVec2f(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    public struct RVec4f
    {
        public float X;
        public float Y;
        public float W;
        public float H;

        public RVec4f(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }
    }
}
