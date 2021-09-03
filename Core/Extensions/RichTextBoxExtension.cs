
using System;
using System.Drawing;
using System.Windows.Forms;

using Core.Common;

namespace Core.Extensions
{
    public static class RichTextBoxExtension
    {

        public static bool AutoScroll { get; set; } = true;

        public static void ScrollToEnd(this RichTextBox richTextBox)
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.ScrollToCaret();
        }

        public static void AppendLineThreadSafe(this RichTextBox richTextBox, string s)
        {
            AppendLineThreadSafe(richTextBox, s, Color.Black);
        }

        public static void AppendLineThreadSafe(this RichTextBox richTextBox, string s, Color color)
        {
            AppendTextThreadSafe(richTextBox, s + Environment.NewLine, color);
        }

        public static void AppendTextThreadSafe(this RichTextBox richTextBox, string s)
        {
            AppendTextThreadSafe(richTextBox, s, Color.Black);
        }

        public static void AppendTextThreadSafe(this RichTextBox richTextBox, string s, Color color)
        {
            if (richTextBox == null)
            {
                return;
            }
            if (richTextBox.InvokeRequired)
            {
                if (richTextBox.IsDisposed) return;
                richTextBox.Invoke(new Action<RichTextBox, string, Color>(AppendTextThreadSafe), richTextBox, s, color);
            }
            else
            {
                if (richTextBox.IsDisposed) return;
                richTextBox.SelectionColor = color;

                int savedVpos = Win32API.GetScrollPos(richTextBox.Handle, Win32API.SB_VERT);
                richTextBox.AppendText(s);
                if (AutoScroll)
                {
                    int VSmin, VSmax;
                    Win32API.GetScrollRange(richTextBox.Handle, Win32API.SB_VERT, out VSmin, out VSmax);
                    int sbOffset = (int)((richTextBox.ClientSize.Height - SystemInformation.HorizontalScrollBarHeight) / (richTextBox.Font.Height));
                    savedVpos = VSmax - sbOffset;
                }
                Win32API.SetScrollPos(richTextBox.Handle, Win32API.SB_VERT, savedVpos, true);
                Win32API.PostMessageA(richTextBox.Handle, Win32API.WM_VSCROLL, Win32API.SB_THUMBPOSITION + 0x10000 * savedVpos, 0);
            }
        }
    }
}
