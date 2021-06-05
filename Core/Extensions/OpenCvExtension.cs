
using System;
using SysSize = System.Drawing.Size;
using CvSize = OpenCvSharp.Size;
using SysRect = System.Drawing.Rectangle;
using CvRect = OpenCvSharp.Rect;

using OpenCvSharp;

namespace Core.Extensions
{
    public static class OpenCvExtension
    {


        public static Mat ToGray(this Mat source)
        {
            var gray = new Mat();
            var channels = source.Channels();
            var code = channels == 4 ? ColorConversionCodes.BGRA2GRAY : ColorConversionCodes.BGR2GRAY;
            Cv2.CvtColor(source, gray, code);
            return gray;
        }

        public static Mat ToBinary(this Mat gray, int threshold)
        {
            var bin = new Mat();
            Cv2.Threshold(gray, bin, threshold, 255, ThresholdTypes.Binary);
            return bin;
        }

        public static SysSize ToSysSize(this CvSize size)
        {
            return new SysSize(size.Width, size.Height);
        }

        public static CvSize ToCvSize(this SysSize size)
        {
            return new CvSize(size.Width, size.Height);
        }

        public static SysRect ToSysRect(this CvRect rect)
        {
            return new SysRect(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static CvRect ToCvRect(this SysRect rect)
        {
            return new CvRect(rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}
