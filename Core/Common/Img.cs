using System;
using System.Collections.Generic;
using System.Drawing;
using SysSize = System.Drawing.Size;

using Core.Emulators;

using OpenCvSharp;
using OpenCvSharp.Extensions;
using CvSize = OpenCvSharp.Size;


namespace Core.Common
{
    public class Img
    {

        public Img(Image image) : this(new Bitmap(image))
        {
        }

        public Img(Bitmap bitmap) : this(bitmap.ToMat())
        {
        }

        public Img(Mat mat)
        {
            MT = mat;
        }

        private Mat MT { get; set; }

        public int Width { get { return MT.Width; } }

        public int Height { get { return MT.Height; } }

        public SysSize Size { get { return new SysSize(Width, Height); } }

        private CvSize GetCvSize()
        {
            return new CvSize(Width, Height);
        }

        private CvSize GetScaledCvSize(CvSize size, double scale)
        {
            var wid = size.Width * scale;
            var hei = size.Height * scale;
            return new CvSize(wid, hei);
        }

        public Img GetPartial(Rectangle rect)
        {
            var xRange = new Range(Math.Max(rect.Left, 0), Math.Min(rect.Right, Width));
            var yRange = new Range(Math.Max(rect.Top, 0), Math.Min(rect.Bottom, Height));
            var partial = MT[yRange, xRange];
            return new Img(partial);
        }

        public Img GetPartial(RVec4f rf)
        {
            var rect = Size * rf;
            return GetPartial(rect);
        }

        public Img GetScaled(double scale)
        {
            var size = GetScaledCvSize(GetCvSize(), scale);
            var scaled = MT.Resize(size);
            return new Img(scaled);
        }

        public void Save(string filePath)
        {
            MT.SaveImage(filePath);
        }

        public Bitmap ToBitmap()
        {
            return MT.ToBitmap();
        }

        public Mat ToMat()
        {
            return MT;
        }
    }
}
