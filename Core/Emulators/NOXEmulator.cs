using System;
using System.Diagnostics;
using System.IO;

using Core.Common;
using Core.Extensions;

namespace Core.Emulators
{
    public class NOXEmulator : Emulator
    {
        public override string Name { get { return "夜神模拟器"; } }

        public override int AdbPort { get; } = 62001;

        public override Process GetMainProcess()
        {
            return GetProcessByName("Nox");
        }

        public override IntPtr GetMainWindowHandle()
        {
            AssertAlive();
            var proc = GetMainProcess();
            var hWnd = Win32API.FindWindowEx(proc.MainWindowHandle, IntPtr.Zero, null, "ScreenBoardClassWindow");
            return hWnd;
        }

        public override string GetAdbExePath()
        {
            AssertAlive();
            var dirPath = GetMainProcess().GetMainModuleDirectoryPath();
            var refPath = $"{dirPath}/nox_adb.exe";
            var path = Path.GetFullPath(refPath);
            return path;
        }
    }
}
