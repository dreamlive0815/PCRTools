using System;
using System.IO;
using System.Windows.Forms;

using Newtonsoft.Json;

using Core.Emulators;
using Core.PCR;
using EventSystem;

namespace Core.Common
{

    public class Config
    {
        public Config()
        {
        }

#if DEBUG
        public bool Debug { get; set; } = true;
#else
        public bool Debug { get; set; } = false;
#endif

        public PCRRegion PCRRegion { get; set; } = PCRRegion.Taiwan;

        public Type DefaultEmulatorType { get; private set; }

        public void SetDefaultEmulator(Emulator emulator)
        {
            emulator.AssertAlive();
            DefaultEmulatorType = emulator.GetType();
        }
    }

    public abstract class ConfigMgr
    {
        private static ConfigMgr instance;

        public static ConfigMgr GetInstance()
        {
            instance = instance ?? new JsonConfigMgr();
            return instance;
        }

        protected ConfigMgr()
        {
            Config = LoadConfig();
        }

        public Config Config { get; protected set; }

        protected abstract Config LoadConfig();

        public abstract void SaveConfig();
    }

    public class JsonConfigMgr : ConfigMgr
    {

        private readonly string JSON_PATH = "./config.json";

        public JsonConfigMgr()
        {
        }

        protected override Config LoadConfig()
        {
            if (!File.Exists(JSON_PATH))
                return new Config();
            var content = File.ReadAllText(JSON_PATH);
            var config = JsonConvert.DeserializeObject<Config>(content);
            return config;
        }

        public override void SaveConfig()
        {
            var content = JsonConvert.SerializeObject(Config, Formatting.Indented);
            File.WriteAllText(JSON_PATH, content);
        }
    }

    public static class ConfigUITool
    {
        public static void AddConfigItemsToMenuStrip(MenuStrip menuStrip)
        {
            var config = ConfigMgr.GetInstance().Config;

            menuStrip.SuspendLayout();

            var settingItems = new ToolStripMenuItem();
            settingItems.Text = "设置(&S)";
            menuStrip.Items.Add(settingItems);

            var regionItems = new ToolStripMenuItem();
            regionItems.Text = "区域";
            settingItems.DropDownItems.Add(regionItems);
            var refreshRegionCheckStatus = new Action(() =>
            {
                foreach (ToolStripMenuItem item in regionItems.DropDownItems)
                {
                    var bChecked = item.Text == config.PCRRegion.ToString();
                    item.Checked = bChecked;
                    regionItems.Text = bChecked ? $"区域: {item.Text}" : regionItems.Text;
                }
            });
            foreach (var name in Enum.GetNames(typeof(PCRRegion)))
            {
                var regionItem = new ToolStripMenuItem();
                regionItem.Text = name;
                regionItems.DropDownItems.Add(regionItem);
                regionItem.Click += (s, e) => {
                    config.PCRRegion = (PCRRegion) Enum.Parse(typeof(PCRRegion), regionItem.Text);
                    ConfigMgr.GetInstance().SaveConfig();
                    refreshRegionCheckStatus();
                    EventMgr.FireEvent("PCRRegionChanged", null);
                };
            }
            refreshRegionCheckStatus();

            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
        }
    }
}
