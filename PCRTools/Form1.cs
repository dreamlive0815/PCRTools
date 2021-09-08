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
using Core.Script;

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

            Emulator.AssertDefaultAliveAndInit();
            var cap = Emulator.Default.GetScreenCapture();
            var img = new Img(cap);
            
            var part = img.GetPartial(new RVec4f(0, 0, 0.5, 0.5));
            
            for (var r = 0; r < part.Height; r++)
            {
                for (var c = 0; c < part.Width; c++)
                {
                    part.SetColor(r, c, System.Drawing.Color.Black);
                }
            }
            part.Show("111");
            img.Show("222");

            //TestScript();
        }

        void TestScript()
        {
            Emulator.AssertDefaultAliveAndInit();
            var script = new Script()
            {
                Identity = "Test",
                Name = "测试脚本",

                Segments = new List<Segment>()
                {
                    new Segment()
                    {
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                MatchKey = "tab_adventure",
                            },
                        },
                        Actions = new List<Core.Script.Action>()
                        {
                            new Core.Script.Action()
                            {
                                OpCodes = { "CLICK_TEMPLATE" },
                            },
                        }
                    },
                },
            };
            script.SetEmulator(Emulator.Default);
            ScriptMgr.GetInstance().RunScript(script);
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
    }
}
