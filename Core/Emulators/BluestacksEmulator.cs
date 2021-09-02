
using System;
using System.Diagnostics;
using System.IO;

using Core.Common;
using Core.Extensions;


namespace Core.Emulators
{
    public class BluestacksEmulator : Emulator
    {
        public override string Name { get { return "蓝叠模拟器"; } }

        public override int AdbPort { get { return 5555; } }

        public override Process GetMainProcess()
        {
            return GetProcessByName("BlueStacksGP");
        }

        public override IntPtr GetMainWindowHandle()
        {
            AssertAlive();
            var proc = GetMainProcess();
            var hWnd = Win32API.FindWindowEx(proc.MainWindowHandle, IntPtr.Zero, null, "HOSTWND");
            return hWnd;
        }

        public override string GetAdbExePath()
        {
            AssertAlive();
            var dirPath = GetMainProcess().GetMainModuleDirectoryPath();
            var refPath = $"{dirPath}/Engine/ProgramFiles/HD-Adb.exe";
            var path = Path.GetFullPath(refPath);
            return path;
        }
    }
}
