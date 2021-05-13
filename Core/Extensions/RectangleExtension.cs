using System;
using System.Drawing;

namespace Core.Extensions
{
    public static class RectangleExtension
    {
        public static Point GetCenterPoint(this Rectangle rect)
        {
            var x = 0.5 * (rect.Left + rect.Right);
            var y = 0.5 * (rect.Top + rect.Bottom);
            return new Point((int)x, (int)y);
        }
    }
}
