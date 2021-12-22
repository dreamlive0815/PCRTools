using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SysAction = System.Action;

using Core.Common;
using Core.Emulators;
using Core.Extensions;
using Core.Model;
using Core.PCR;
using Core.Script;

using EventSystem;
using OpenCvSharp;

namespace PCRTools
{
    public partial class Form1 : Form
    {

        private static readonly int MAX_SCRIPT_MENU_COUNT = 10;

        public Form1()
        {
            InitializeComponent();
            ConfigUITool.AddConfigItemsToMenuStrip(menuStrip1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ScriptGenerator.GenStagelineAutoBattle().Save();
            RegisterEvents();

            ScriptMgr.GetInstance();

            RefreshText();
            RefreshScriptMenuItems();

            //Utils.PrintProcessesInfoToFile("1.txt");
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
            catch { }
            return sb.ToString();
        }


        string GetRegionInfo()
        {
            return "区域:" + ConfigMgr.GetConfig().Region.ToString();
        }

        void RefreshText()
        {
            Invoke(new System.Action(() =>
            {
                Text = GetTitle();
            }));
        }

        void RegisterEvents()
        {
            EventMgr.RegisterListener(EventKeys.ConfigEmulatorTypeChanged, (args) => { RefreshText(); });
            EventMgr.RegisterListener(EventKeys.ConfigRegionChanged, (args) => { RefreshText(); });
            EventMgr.RegisterListener(EventKeys.ScriptMetaInfosChanged, (args) => { Invoke(new SysAction(RefreshScriptMenuItems)); });

            Logger.GetInstance().OnDebug += OnLoggerDebug;
            Logger.GetInstance().OnInfo += OnLoggerInfo;
            Logger.GetInstance().OnWarn += OnLoggerWarn;
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

        private void OnLoggerWarn(string tag, string msg)
        {
            txtOutput.AppendLineThreadSafe($"[WARN][{tag}]{msg}", System.Drawing.Color.Orange);
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
                var filePath = "screenshot.png";
                screenshot.Save(filePath);
                Utils.SelectFileInExplorer(filePath);
            }
        }

        private void menuPCRArenaTimer_Click(object sender, EventArgs e)
        {
            new FrmPCRArenaTimer().Show();
        }

        string GetEmulatorDetail()
        {
            if (Emulator.Default == null)
                return "未设置模拟器";
            var emulator = Emulator.Default;
            var sb = new StringBuilder();
            sb.Append("名称: ");
            sb.Append(emulator.Name);
            sb.Append(Environment.NewLine);
            sb.Append("状态: ");
            sb.Append(emulator.IsAlive ? "在线" : "离线");
            sb.Append(Environment.NewLine);
            if (!emulator.IsAlive)
                return sb.ToString();
            emulator.Init();
            sb.Append("分辨率: ");
            sb.Append(emulator.GetResolution());
            sb.Append(Environment.NewLine);
            sb.Append("实际窗口大小: ");
            if (emulator.IsAreaValid())
                sb.Append(emulator.Area);
            else
                sb.Append("区域不合法");
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }

        private void menuEmulatorInfo_Click(object sender, EventArgs e)
        {
            new FrmRuntimeInfo() { GetRuntimeInfoFunc = GetEmulatorDetail }.Show();
        }

        void RefreshScriptMenuItems()
        {
            menuScripts.DropDownItems.Clear();

            var items = menuScripts.DropDownItems;
            var stopScriptMenu = new ToolStripMenuItem("停止脚本");
            stopScriptMenu.Click += menuStopScript_Click;
            items.Add(stopScriptMenu);
            items.Add(new ToolStripSeparator());

            var count = 0;
            var infos = ConfigMgr.GetConfig().ScriptMetaInfos;
            foreach (var info in infos)
            {
                if (!info.Enabled)
                    continue;
                var script = ScriptMgr.GetInstance().GetScript(info.Identity);
                var scriptItem = new ScriptMenuItem(script.Name);
                scriptItem.ScriptMetaInfo = info;
                scriptItem.Click += menuScriptItem_Click;
                items.Add(scriptItem);
                count++;
                if (count >= MAX_SCRIPT_MENU_COUNT)
                    break;
            }

            items.Add(new ToolStripSeparator());
            var scriptMgrMenu = new ToolStripMenuItem("脚本管理器");
            scriptMgrMenu.Click += menuScriptMgr_Click;
            items.Add(scriptMgrMenu);


#if DEBUG
            items.Add(new ToolStripSeparator());
            var testScriptMenu = new ToolStripMenuItem("脚本测试");
            testScriptMenu.Click += TestScriptMenu_Click;
            items.Add(testScriptMenu);
#endif
        }

        private void TestScriptMenu_Click(object sender, EventArgs e)
        {
            Emulator.AssertDefaultAliveAndInit();
            var script = ScriptGenerator.GenTestScript();
            script.Save();
            script.SetEmulator(Emulator.Default);
            ScriptMgr.GetInstance().RunDefaultScript(script);
        }

        private void menuScriptItem_Click(object sender, EventArgs e)
        {
            Emulator.AssertDefaultAliveAndInit();
            var menuItem = (ScriptMenuItem)sender;
            var identity = menuItem.ScriptMetaInfo.Identity;
            var script = ScriptMgr.GetInstance().GetScript(identity);
            script.SetEmulator(Emulator.Default);
            ScriptMgr.GetInstance().RunDefaultScript(script);
            menuItem.Text = script.Name + "[Running]";
        }

        private void menuStopScript_Click(object sender, EventArgs e)
        {
            ScriptMgr.GetInstance().StopDefaultScript();
            RefreshScriptMenuItems();
        }

        private void menuScriptMgr_Click(object sender, EventArgs e)
        {
            new FrmScriptManager().Show();
        }
    }

    public class ScriptMenuItem : ToolStripMenuItem
    {
        public ScriptMenuItem(string text) : base(text)
        {
        }

        public ScriptMetaInfo ScriptMetaInfo { get; set; }
    }
}
