using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Core.Common;
using Core.Emulators;

namespace GetVec
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ConfigUITool.AddConfigItemsToMenuStrip(menuStrip1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var configMgr = ConfigMgr.GetInstance();
            var config = configMgr.Config;
            //config.DefaultEmulatorType = typeof(MuMuEmulator);
            configMgr.SaveConfig();
        }
    }
}
