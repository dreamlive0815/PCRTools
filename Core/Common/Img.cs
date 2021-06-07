using System;
using System.Collections.Generic;
using System.Drawing;
using SysSize = System.Drawing.Size;

using Core.Emulators;
using Core.Extensions;

using OpenCvSharp;
using OpenCvSharp.Extensions;
using CvPoint = OpenCvSharp.Point;
using CvSize = OpenCvSharp.Size;


namespace Core.Common
{
    public class Img : IDisposable
    {

        public Img(Image image) : this(new Bitmap(image))
        {
        }

        public Img(Bitmap bitmap) : this(bitmap.ToMat())
        {
        }

        public Img(Mat mat)
        {
            if (mat.Channels() > 3)
            {
                mat = mat.CvtColor(ColorConversionCodes.RGBA2RGB);
            }
            MT = mat;
        }

        public Img(string filePath) : this(new Mat(filePath))
        {
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

        public Img GetResized(SysSize size)
        {
            var resized = MT.Resize(size.ToCvSize());
            return new Img(resized);
        }

        public ImgMatchResult Match(Img searchImg, double threshold)
        {
            var source = MT;
            var search = searchImg.MT;
            var res = new Mat();
            Cv2.MatchTemplate(source, search, res, TemplateMatchModes.CCoeffNormed);
            double minVal, maxVal;
            CvPoint minLoc, maxLoc;
            Cv2.MinMaxLoc(res, out minVal, out maxVal, out minLoc, out maxLoc);
            var r = new ImgMatchResult()
            {
                Threshold = threshold,
                Maxval = maxVal,
                Success = maxVal >= threshold,
                MatchedRect = new Rectangle(maxLoc.X, maxLoc.Y, search.Width, search.Height),
            };
            if (r.Success && ConfigMgr.GetConfig().Debug)
            {
                source.SaveImage("source.png");
                search.SaveImage("search.png");
                Console.WriteLine(maxVal);

                //var combine = new Mat(new CvSize(source.Width, source.Height + search.Height), MatType.MakeType(source.Depth(), source.Channels()));
                //source.CopyTo(combine[new Range(0, source.Height), new Range(0, source.Width)]);
                //var xOff = (source.Width - search.Width) / 2;
                //search.CopyTo(combine[new Range(source.Height, source.Height + search.Height), new Range(xOff, search.Width + xOff)]);
                //Cv2.ImShow("combine", combine);

                //Cv2.Circle(source, maxLoc.X + search.Width / 2, maxLoc.Y + search.Height / 2, 25, Scalar.Red);
                //Cv2.ImShow("ImgMatch", source);
            }
            return r;
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

        public void Dispose()
        {
            MT?.Dispose();
            MT = null;
        }
    }

    public class ImgMatchResult
    {
        public double Threshold { get; set; }

        public double Maxval { get; set; }

        public bool Success { get; set; }

        public Rectangle MatchedRect { get; set; }
    }
}
