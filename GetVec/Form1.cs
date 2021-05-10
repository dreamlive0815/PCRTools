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
using EventSystem;

namespace GetVec
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
            InitEmulator();
            RefreshTitle();
            RegisterEvents();

            emulator.GetResolution();
            emulator.GetResolution();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ConfigMgr.GetInstance().SaveConfig();
        }

        void InitEmulator()
        {
            emulator = Emulator.GetInstanceByType(ConfigMgr.GetConfig().EmulatorType);
        }

        string GetEmulatorInfo()
        {
            if (emulator == null)
                return "未选择模拟器";
            var msg = $"模拟器: {emulator.Name}";
            if (emulator.Alive)
            {
                msg += "[ON]";
            }
            else
                msg += "[OFF]";
            return msg;
        }

        string GetRegionInfo()
        {
            return $"区域: {ConfigMgr.GetConfig().Region}";
        }

        void RefreshTitle()
        {
            Invoke(new Action(() => {
                Text = $"{GetEmulatorInfo()}  {GetRegionInfo()}";
            }));
        }

        void RegisterEvents()
        {
            EventMgr.RegisterListener(EventKeys.ConfigEmulatorTypeChanged, (args) =>
            {
                InitEmulator();
                RefreshTitle();
            });
            EventMgr.RegisterListener(EventKeys.ConfigRegionChanged, (args) => { RefreshTitle(); });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshTitle();
        }
    }
}
