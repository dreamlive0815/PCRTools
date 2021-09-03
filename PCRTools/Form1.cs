using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Core.Common;
using Core.Emulators;
using Core.Extensions;
using Core.Model;
using Core.PCR;

using EventSystem;
using OpenCvSharp;


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
            return emulator.Name + ":" + (emulator.IsAlive ? "在线ON" : "离线OFF");
        }

        string GetTitle()
        {
            var sb = new StringBuilder();
            try
            {
                sb.Append(GetEmulatorInfo());
                sb.Append("  ");
                sb.Append(GetRegionInfo());

            }
            catch (Exception e) { }
            return sb.ToString();
        }


        string GetRegionInfo()
        {
            return "区域:" + ConfigMgr.GetConfig().Region.ToString();
        }

        void RefreshText()
        {
            Invoke(new Action(() =>
            {
                Text = GetTitle();
            }));
        }

        void RegisterEvents()
        {
            EventMgr.RegisterListener(EventKeys.ConfigEmulatorTypeChanged, (args) => { RefreshText(); });
            EventMgr.RegisterListener(EventKeys.ConfigRegionChanged, (args) => { RefreshText(); });

            Logger.GetInstance().OnDebug += OnLoggerDebug;
            Logger.GetInstance().OnInfo += OnLoggerInfo;
            Logger.GetInstance().OnError += OnLoggerError;
        }

        private void OnLoggerDebug(string tag, string msg)
        {
            txtOutput.AppendLineThreadSafe($"[DEBUG][{tag}]{msg}");
        }

        private void OnLoggerInfo(string tag, string msg)
        {
            txtOutput.AppendLineThreadSafe($"[INFO][{tag}]{msg}");
        }

        private void OnLoggerError(string tag, string msg)
        {
            txtOutput.AppendLineThreadSafe($"[ERROR][{tag}]{msg}", System.Drawing.Color.Red);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshText();
        }

        private void menuPCRArena_Click(object sender, EventArgs e)
        {
            new FrmPCRArena().Show();
        }

        private void menuClearOutput_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
        }

        private void menuConnectAdbServer_Click(object sender, EventArgs e)
        {
            Emulator.AssertDefaultAlive();
            Emulator.Default.ConnectToAdbServer();
        }

        private void menuRestartAdbServer_Click(object sender, EventArgs e)
        {
            Emulator.AssertDefaultAlive();
            Emulator.Default.RestartAdbServer();
        }

        private void menuTestTap_Click(object sender, EventArgs e)
        {
            Emulator.AssertDefaultAliveAndInit();
            Emulator.Default.DoTap(new PVec2f(0.5f, 0.5f));
        }

        private void menuScreenshot_Click(object sender, EventArgs e)
        {
            Emulator.AssertDefaultAlive();
            using (var screenshot = Emulator.Default.GetScreenCapture())
            {
                screenshot.Save("screenshot.png");
            }
        }
    }
}
