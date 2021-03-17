using System;
using System.Drawing;

using Core.Common;
using System.Diagnostics;

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

        protected Process GetProcessByName(string name)
        {
            var processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                var procName = process.ProcessName;
                if (procName.Contains(name))
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
