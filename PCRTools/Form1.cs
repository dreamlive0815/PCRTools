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
using Core.Model;
using Core.PCR;

using EventSystem;


namespace PCRTools
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
            RefreshText();
            RegisterEvents();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConfigMgr.GetInstance().SaveConfig();
        }

        string GetEmulatorInfo()
        {
            var emulator = Emulator.Default;
            if (emulator == null)
                return "未选择模拟器";
            return emulator.Name + (emulator.IsAlive ? ": 在线ON" : ": 离线OFF");
        }

        string GetRegionInfo()
        {
            return "区域: " + ConfigMgr.GetConfig().Region.ToString();
        }

        void RefreshText()
        {
            Invoke(new Action(() =>
            {
                var s = $"[{GetEmulatorInfo()}][{GetRegionInfo()}]";
                Text = s;
            }));
        }

        void RegisterEvents()
        {
            EventMgr.RegisterListener(EventKeys.ConfigEmulatorTypeChanged, (args) => { RefreshText(); });
            EventMgr.RegisterListener(EventKeys.ConfigRegionChanged, (args) => { RefreshText(); });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshText();
        }

        private void menuPCRArena_Click(object sender, EventArgs e)
        {
            new FrmPCRArena().Show();
        }
    }
}
