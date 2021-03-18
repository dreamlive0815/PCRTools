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
    public class NOXEmulator : Emulator
    {
        public override string Name { get { return "NOXEmulator"; } }

        public override Rectangle Area
        {
            get
            {
                if (!Alive)
                    throw new Exception(AheadWithName("进程不存在"));
                var proc = GetMainProcess();
                var hwnd = Win32API.FindWindowEx(proc.MainWindowHandle, IntPtr.Zero, null, "ScreenBoardClassWindow");
                var rect = Win32API.GetWindowRect(hwnd);
                if (!rect.Valid || rect.Width < 10 || rect.Height < 10)
                    throw new Exception(AheadWithName("主窗口尺寸不合法"));
                return rect.ToRectangle();
            }
        }

        public override Process GetMainProcess()
        {
            return GetProcessByName("Nox");
        }

    }
}
