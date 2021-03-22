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
using System.Reflection;

namespace PCRTools
{
    public partial class Form1 : Form
    {

        Emulator emulator;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //emulator = new NOXEmulator();
            emulator = new MuMuEmulator();
            emulator.ConnectToAdbServer();
            var before = DateTime.Now;
            
            emulator.DoTap(new PVec2f(0.3656f, 0.6859f));
            var span = DateTime.Now - before;
            Console.WriteLine(span.TotalMilliseconds);
            var configMgr = ConfigMgr.GetInstance();
            var config = configMgr.Config;

            var list = Emulator.GetEmulatorTypes();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConfigMgr.GetInstance().SaveConfig();
        }
    }
}
