using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        public static void PrintProcessNamesToFile(string filePath)
        {
            var sb = new StringBuilder();
            foreach (var process in Process.GetProcesses())
            {
                sb.AppendLine(process.ProcessName);
            }
            File.WriteAllText(filePath, sb.ToString());
        }
    }
}
