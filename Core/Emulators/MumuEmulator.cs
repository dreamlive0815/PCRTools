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
    public class MuMuEmulator : Emulator
    {
        public override string Name { get; } = "MuMuEmulator";

        public override Rectangle Area
        {
            get
            {
                if (!Alive)
                    throw new Exception(AheadWithName("进程不存在"));
                var proc = GetMainProcess();
                var hWnd = Win32API.FindWindowEx(proc.MainWindowHandle, IntPtr.Zero, null, null);
                var title = Win32API.GetWindowTitle(hWnd);
                if (!title.Contains("NemuPlayer"))
                {
                    throw new Exception(AheadWithName("获取主窗口失败"));
                }
                var rect = Win32API.GetWindowRect(hWnd);
                if (!rect.Valid || rect.Width < 10 || rect.Height < 10)
                    throw new Exception(AheadWithName("主窗口尺寸不合法"));
                return rect.ToRectangle();
            }
        }

        public override Process GetMainProcess()
        {
            return GetProcessByName("NemuPlayer");
        }
        
    }
}
