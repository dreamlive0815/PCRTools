
using System;
using System.Diagnostics;
using System.IO;

using Core.Common;
using Core.Extensions;

namespace Core.Emulators
{
    public class MuMuEmulatorEx : Emulator
    {
        public override string Name { get; } = "Mumu手游助手";

        public override int AdbPort { get; } = 7555;

        public override Process GetMainProcess()
        {
            return GetProcessByName("NeumPlayer");
        }

        public override IntPtr GetMainWindowHandle()
        {
            AssertAlive();
            var proc = GetMainProcess();
            var hWnd = Win32API.FindWindowEx(proc.MainWindowHandle, IntPtr.Zero, null, null);
            return hWnd;
        }

        public override string GetAdbExePath()
        {
            AssertAlive();
            var dirPath = GetMainProcess().GetMainModuleDirectoryPath();
            var refPath = $"{dirPath}/../vmonitor/bin/adb_server.exe";
            var path = Path.GetFullPath(refPath);
            return path;
        }
    }
}
