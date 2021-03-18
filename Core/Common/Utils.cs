using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;

namespace Core.Common
{
    public class Utils
    {
        public static Bitmap CaptureScreen(Rectangle rect)
        {

            rect.X = Math.Max(rect.X, 0);
            rect.Y = Math.Max(rect.Y, 0);;
            rect.Width = Math.Max(rect.Width, 1);
            rect.Height = Math.Max(rect.Height, 1);
            var bitmap = new Bitmap(rect.Width, rect.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(rect.X, rect.Y, 0, 0, new Size(rect.Width, rect.Height));
            }
            return bitmap;
        }


        public static string GetMainModuleFilePath(int processId)
        {
            string wmiQueryString = "SELECT ProcessId, ExecutablePath FROM Win32_Process WHERE ProcessId = " + processId;
            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            {
                using (var results = searcher.Get())
                {
                    ManagementObject mo = results.Cast<ManagementObject>().FirstOrDefault();
                    if (mo != null)
                    {
                        return (string)mo["ExecutablePath"];
                    }
                }
            }
            return null;
        }

        public static void PrintProcessesInfoToFile(string filePath)
        {
            var sb = new StringBuilder();
            foreach (var process in Process.GetProcesses())
            {
                sb.AppendLine($"{process.ProcessName} {GetMainModuleFilePath(process.Id)}");
            }
            File.WriteAllText(filePath, sb.ToString());
        }

        public static void PrintAllChildWindows(IntPtr parentHandle)
        {
            var childHandle = IntPtr.Zero;
            var sb = new StringBuilder();
            while (true)
            {
                var hWnd = Win32API.FindWindowEx(parentHandle, childHandle, null, null);
                if (hWnd == IntPtr.Zero)
                    break;
                var title = Win32API.GetWindowTitle(hWnd);
                var rect = Win32API.GetWindowRect(hWnd);
                childHandle = hWnd;
                sb.AppendLine($"{hWnd} {title} {rect}");
            }
            Console.Write(sb.ToString());
        }
    }
}
