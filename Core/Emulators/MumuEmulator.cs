using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Common;

namespace Core.Emulators
{
    public class MumuEmulator : Emulator
    {
        public override string Name { get; } = "MumuEmulator";

        public override bool Alive
        {
            get
            {
                var proc = GetMumuProcess();
                return proc != null && !proc.HasExited;
            }
        }

        public override Rectangle Area
        {
            get
            {
                if (!Alive)
                    throw new Exception("Mumu模拟器进程不存在");
                var proc = GetMumuProcess();
                var hWnd = Win32API.FindWindowEx(proc.MainWindowHandle, IntPtr.Zero, null, null);
                var title = Win32API.GetWindowTitle(hWnd);
                if (!title.Contains("NemuPlayer"))
                {
                    throw new Exception("获取Mumu模拟器尺寸失败");
                }
                var rect = Win32API.GetWindowRect(hWnd);
                if (!rect.Valid || rect.Width < 10 || rect.Height < 10)
                    throw new Exception("Mumu模拟器尺寸不合法");
                return rect.ToRectangle();
            }
        }

        public Process GetMumuProcess()
        {
            var processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                var procName = process.ProcessName;
                if (procName.Contains("NemuPlayer"))
                {
                    return process;
                }
            }
            return null;
        }

    }
}
