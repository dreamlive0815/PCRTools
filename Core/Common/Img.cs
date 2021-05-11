using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;
using OpenCvSharp.Extensions;


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
