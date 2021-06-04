using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Core.Common;
using Core.Emulators;
using Core.Extensions;
using Core.PCR;

using EventSystem;

using OpenCvSharp;

namespace PCRTools
{
    public partial class Form1 : Form
    {

        Emulator emulator;

        public Form1()
        {
            InitializeComponent();
            ConfigUITool.AddConfigItemsToMenuStrip(menuStrip1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EventMgr.RegisterListener(EventKeys.ConfigRegionChanged, (args) =>
            {
                Invoke(new Action(() => {
                    Text = ConfigMgr.GetConfig().Region.ToString();
                }));
            });

            Test();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConfigMgr.GetInstance().SaveConfig();
        }


        int GetSquareSize(OpenCvSharp.Point p1, OpenCvSharp.Point p0)
        {
            var dx = p1.X - p0.X;
            var dy = p1.Y - p0.Y;
            return dx * dx + dy * dy;
        }

        bool IsSquare(OpenCvSharp.Point[] approx)
        {
            for (int j = 2; j < 5; j++)
            {
                var p1 = approx[j % 4];
                var p2 = approx[j - 2];
                var p0 = approx[j - 1];
                var ratio = 1.0 * GetSquareSize(p1, p0) / GetSquareSize(p2, p0);
                if (ratio < 0.8 || ratio > 1.2)
                    return false;
            }
            return true;

        }

        void Test()
        {
            var start = DateTime.Now;
            var img = Cv2.ImRead("33.png");
            img = img.GaussianBlur(new OpenCvSharp.Size(3, 3), 0);
            var gray = img.ToGray();
            //Cv2.ImShow("gray", gray);
            var canny = gray.Canny(30, 100);
            //Cv2.ImShow("bin", canny);
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;
            canny.FindContours(out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
            var list = new List<Tuple<double, OpenCvSharp.Point[]>>();
            foreach (var contour in contours)
            {
                var borderLen = Cv2.ArcLength(contour, true);
                var approx = Cv2.ApproxPolyDP(contour, 0.02 * borderLen, true);

                if (approx.Length == 4 && Cv2.IsContourConvex(approx))
                {
                    if (borderLen > 100 && IsSquare(approx))
                    {
                        list.Add(new Tuple<double, OpenCvSharp.Point[]>(borderLen, approx));
                    }
                }
            }
            list.Sort((a, b) =>
            {
                return a.Item1.CompareTo(b.Item1);
            });
            var listlist = new List<List<Tuple<double, OpenCvSharp.Point[]>>>();
            var tempList = new List<Tuple<double, OpenCvSharp.Point[]>>() { list[0] };
            for (var i = 1; i < list.Count; i++)
            {
                var ratio = list[i].Item1 / list[i - 1].Item1;
                if (ratio > 1.1)
                {
                    listlist.Add(tempList);
                    tempList = new List<Tuple<double, OpenCvSharp.Point[]>>();
                }
                tempList.Add(list[i]);
            }
            listlist.Add(tempList);
            var maxCount = listlist.Max(x => x.Count);
            var maxCountList = listlist.Where(x => x.Count == maxCount).FirstOrDefault();
            var filteredContours = maxCountList.Select(x => x.Item2).ToList();
            img.DrawContours(filteredContours, -1, Scalar.Red, 2);
            Cv2.ImShow("img", img);
            var averArcLen = maxCountList.Average(x => x.Item1);
            Console.WriteLine(averArcLen);

            var icon = Unit.GetIconResource("1001", 6).ToImg();
            icon = icon.GetScaled(averArcLen * 0.25 / 128);
            Cv2.ImShow("iconMat", icon.ToMat());
            var end = DateTime.Now;
            var span = end - start;
            Console.WriteLine(span);
        }
    }
}
