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

    }
}
