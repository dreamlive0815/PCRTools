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

        public abstract Rectangle Area { get; }

        public abstract Process GetMainProcess();

        protected string AheadWithName(string str)
        {
            return $"[{Name}]{str}";
        }

        protected Process GetProcessByName(string name)
        {
            return ProcessExtension.GetProcessByName(name);
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

        public Bitmap CaptureScreen(Rectangle rect)
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
