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
            //emulator.GetScreenCapture().Save("nox.png");
            //Utils.SelectFileInExplorer(emulator.GetAdbExePath());
            emulator.ConnectToAdbServer();
            emulator.GetResolution();
            
            
        }

    }
}
