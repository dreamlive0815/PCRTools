using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{

    public class Win32API
    {

        public const int SB_VERT = 0x1;
        public const int SB_THUMBPOSITION = 0x4;
        public const int WM_VSCROLL = 0x115;


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, out Win32Rect rect);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetScrollPos(IntPtr hWnd, int nBar);

        [DllImport("user32.dll")]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern bool PostMessageA(IntPtr hWnd, int nBar, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);

        public static Win32Rect GetWindowRect(IntPtr hWnd)
        {
            var rect = new Win32Rect();
            GetWindowRect(hWnd, out rect);
            return rect;
        }

        public static string GetWindowTitle(IntPtr hWnd)
        {
            int length = GetWindowTextLength(hWnd);
            StringBuilder windowName = new StringBuilder(length + 1);
            GetWindowText(hWnd, windowName, windowName.Capacity);
            return windowName.ToString();
        }
    }


    public struct Win32Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public int Width { get { return Right - Left; } }

        public int Height { get { return Bottom - Top; } }

        public bool Valid
        {
            get { return Left >= 0 && Top >= 0 && Right >= 0 && Bottom >= 0; }
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(Left, Top, Width, Height);
        }
    }
}
