using System;
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

        private Size resolution;

        public Size GetResolution()
        {
            if (!resolution.IsEmpty)
                return resolution;
            var output = AdbShell("wm size");
            var match = Regex.Match(output, "(\\d+)x(\\d+)");
            if (!match.Success)
                throw new Exception(AheadWithName("获取模拟器分辨率失败"));
            var size = new Size(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            resolution = size;
            return size;
        }
    }

    public struct EVec2f
    {
        public float X;
        public float Y;

        public EVec2f(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    public struct EVec4f
    {

    }
}
