using System;
using System.Diagnostics;
using System.IO;

using Core.Common;
using Core.Extensions;

namespace Core.Emulators
{
    public class LeidianEmulator : Emulator
    {
        public override string Name { get { return "雷电模拟器"; } }

        public override int AdbPort { get; } = 5555;

        public override Process GetMainProcess()
        {
            return GetProcessByName("dnplayer");
        }

        public override IntPtr GetMainWindowHandle()
        {
            AssertAlive();
            var proc = GetMainProcess();
            var hWnd = Win32API.FindWindowEx(proc.MainWindowHandle, IntPtr.Zero, null, "TheRender");
            return hWnd;
        }

        protected override string GetSpecificIdentity()
        {
            var device = GetFirstOnlineDevice();
            if (device != null)
            {
                return device.SpecificIdentity;
            }
            return base.GetSpecificIdentity();
        }

        public override string GetAdbExePath()
        {
            AssertAlive();
            var dirPath = GetMainProcess().GetMainModuleDirectoryPath();
            var refPath = $"{dirPath}/adb.exe";
            var path = Path.GetFullPath(refPath);
            return path;
        }
    }
}
