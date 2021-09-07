using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Core.Attributes;
using Core.Exceptions;

namespace Core.Common
{
    public class Utils
    {

        public static Exception GetInnermostException(Exception e)
        {
            while (e.InnerException != null)
            {
                e = e.InnerException;
            }
            return e;
        }

        public static void HandleError(Exception e)
        {
            e = GetInnermostException(e);
            if (e is HandledException) return;
            MessageBox.Show(GetErrorDescription(e), "Error");
        }

        public static string GetErrorDescription(Exception e)
        {
            e = GetInnermostException(e);
            var desc = e.Message;
            if (ConfigMgr.GetConfig().Debug)
            {
                desc += "\n" + e.StackTrace;
            }
            return desc;
        }

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

        public static List<Type> GetChildTypes<T>(Assembly assembly)
        {
            var list = new List<Type>();
            var types = assembly.GetExportedTypes();
            foreach (var type in types)
            {
                if (Attribute.IsDefined(type, typeof(DisableAttribute)))
                    continue;
                if (type.IsSubclassOf(typeof(T)))
                {
                    list.Add(type);
                }
            }
            return list;
        }

        #region FileSystem

        public static void SelectFileInExplorer(string fullPath)
        {
            fullPath = Path.GetFullPath(fullPath);
            Process.Start("Explorer.exe", $"/select,{fullPath}");
        }

        public static void OpenDirectoryInExplorer(string fullPath)
        {
            fullPath = Path.GetFullPath(fullPath);
            Process.Start("Explorer.exe", $"{fullPath}");
        }

        public static void MakeDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }

        #endregion

        #region Process

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

        #endregion

        #region Math

        public static int GetLargestCommonDivisor(int a, int b)
        {
            var min = Math.Min(a, b);
            var max = Math.Max(a, b);
            var remainder = 0;
            while (min != 0)
            {
                remainder = max % min;
                max = min;
                min = remainder;
            }
            return max;
        }

        #endregion

        public static bool IsAdministrator()
        {
            var current = WindowsIdentity.GetCurrent();
            var windowsPrincipal = new WindowsPrincipal(current);
            return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void AssertAdministrator()
        {
            if (!IsAdministrator())
                throw new Exception("需要管理员权限");
        }

        public static T BinSearch<T>(IList<T> list, T search, Func<T, T, int> comparer)
        {
            var left = 0;
            var right = list.Count - 1;
            var middle = 0;
            while (left <= right)
            {
                middle = (left + right) / 2;
                var middleItem = list[middle];
                var cmp = comparer(middleItem, search);
                if (cmp > 0) //middleItem > search
                    right = middle - 1;
                else if (cmp < 0) //middleItem < search
                    left = middle + 1;
                else
                    return middleItem;
            }
            return default(T);
        }
    }
}
