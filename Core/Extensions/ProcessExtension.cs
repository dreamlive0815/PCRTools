using System;
using System.Diagnostics;
using System.IO;

using Core.Common;


namespace Core.Extensions
{
    public static class ProcessExtension
    {
        public static Process GetProcessByName(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            return processes.Length > 0 ? processes[0] : null;
        }

        public static string GetMainModuleFilePath(this Process process)
        {
            return Utils.GetMainModuleFilePath(process.Id);
        }

        public static string GetMainModuleDirectoryPath(this Process process)
        {
            var filePath = GetMainModuleFilePath(process);
            return Path.GetDirectoryName(filePath);
        }
    }
}
