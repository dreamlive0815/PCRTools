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

        void Test()
        {
            var img = Cv2.ImRead("22.png");
            img = img.GaussianBlur(new OpenCvSharp.Size(3, 3), 0);
            var gray = img.ToGray();
            //Cv2.ImShow("gray", gray);
            var canny = gray.Canny(30, 100);
            Cv2.ImShow("bin", canny);

            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;
            canny.FindContours(out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
            var list = new List<OpenCvSharp.Point[]>();
            foreach (var contour in contours)
            {
                var borderLen = Cv2.ArcLength(contour, true);
                var approx = Cv2.ApproxPolyDP(contour, 0.02 * borderLen, true);

                if (approx.Length == 4 && Cv2.IsContourConvex(approx) && borderLen > 100)
                {
                    list.Add(approx);
                }
            }

            img.DrawContours(list, -1, Scalar.Red, 2);
            Cv2.ImShow("img", img);
        }
    }
}
