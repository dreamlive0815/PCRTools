using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetVec
{
    public partial class FrmInfo : Form
    {
        public FrmInfo()
        {
            InitializeComponent();
        }

        private void FrmInfo_Load(object sender, EventArgs e)
        {

        }

        public Func<string> GetEmulatorInfoFunc { get; set; }

        public Func<string> GetRegionInfoFunc { get; set; }

        public Func<string> GetImageInfoFunc { get; set; }

        string GetInfo(Func<string> func)
        {
            if (func == null)
                return "<未设置>";
            try
            {
                var ret = func();
                return ret;
            }
            catch (Exception e)
            {
                return $"错误: {e.Message}";
            }
        }

        void RefreshInfo()
        {
            txtEmulator.Text = GetInfo(GetEmulatorInfoFunc);
            txtRegion.Text = GetInfo(GetRegionInfoFunc);
            txtImage.Text = GetInfo(GetImageInfoFunc);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshInfo();
        }

        private void FrmInfo_VisibleChanged(object sender, EventArgs e)
        {
            Console.WriteLine(Visible);
        }
    }
}
