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

using CvSize = OpenCvSharp.Size;
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

            var list = Arena.FindUnits(new Img("11.png"));
            foreach (var unit in list)
            {
                Console.WriteLine(unit.Name);
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConfigMgr.GetInstance().SaveConfig();
        }

    }
}
