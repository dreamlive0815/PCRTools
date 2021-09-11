using System;
using System.Collections.Generic;
using System.Drawing;
using SysPoint = System.Drawing.Point;
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

        public SysPoint PositionInRoot { get; set; }

        public SysSize SizeOfRoot { get; set; }

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
            return new Img(partial) {
                PositionInRoot = new SysPoint(PositionInRoot.X + rect.X, PositionInRoot.Y + rect.Y),
                SizeOfRoot = SizeOfRoot.IsEmpty ? Size : SizeOfRoot,
            };
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
                
            };
            if (r.Success)
            {
                r.MatchedRect = new Rectangle(maxLoc.X, maxLoc.Y, search.Width, search.Height);
                r.MatchedRectInRoot = new Rectangle(maxLoc.X + PositionInRoot.X, maxLoc.Y + PositionInRoot.Y, search.Width, search.Height);
                r.SizeOfRoot = SizeOfRoot;
            }
            if (ConfigMgr.GetConfig().Debug)
            {
                Cv2.Circle(source, maxLoc.X + search.Width / 2, maxLoc.Y + search.Height / 2, 25, Scalar.Red);

                source.SaveImage("source.png");
                search.SaveImage("search.png");

                Logger.GetInstance().Debug("ImgMatch", $"ImgMatch: MaxVal={maxVal} Threshold:{threshold} Result={r.Success}");

                //var combine = new Mat(new CvSize(source.Width, source.Height + search.Height), MatType.MakeType(source.Depth(), source.Channels()));
                //source.CopyTo(combine[new Range(0, source.Height), new Range(0, source.Width)]);
                //var xOff = (source.Width - search.Width) / 2;
                //search.CopyTo(combine[new Range(source.Height, source.Height + search.Height), new Range(xOff, search.Width + xOff)]);
                //Cv2.ImShow("combine", combine);
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

        public Color GetColor(int r, int c)
        {
            Color clr;
            if (MT.Channels() == 1)
            {
                var v = MT.Get<byte>(r, c);
                clr = Color.FromArgb(v, v, v);
            }
            else
            {
                var vec3b = MT.Get<Vec3b>(r, c);
                clr = Color.FromArgb(vec3b.Item0, vec3b.Item1, vec3b.Item2);
            }
            return clr;
        }

        public void SetColor(int r, int c, Color color)
        {
            if (MT.Channels() == 1)
            {
                MT.Set(r, c, color.G);
            }
            else
            {
                var vec3b = new Vec3b(color.R, color.G, color.B);
                MT.Set(r, c, vec3b);
            }
        }

        public void Show(string key)
        {
            Cv2.ImShow(key, MT);
        }

        public Img Clone()
        {
            return new Img(MT.Clone())
            {
                PositionInRoot = PositionInRoot,
                SizeOfRoot = SizeOfRoot,
            };
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

        public Rectangle MatchedRectInRoot { get; set; }

        public SysSize SizeOfRoot { get; set; }

        public PVec2f GetMatchedRectCenterVec2f()
        {
            if (SizeOfRoot.Width == 0 || SizeOfRoot.Height == 0)
                throw new Exception("SizeOfRoot is not set");
            var center = MatchedRectInRoot.GetCenterPoint();
            var vec2f = PVec2f.Div(SizeOfRoot, center);
            return vec2f;
        }
    }
}
