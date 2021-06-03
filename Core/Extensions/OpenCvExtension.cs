
using System;

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
    }
}
