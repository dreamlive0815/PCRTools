using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Core.Emulators;
using Core.Extensions;
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

        private Type emulatorType;

        public Type EmulatorType
        {
            get { return emulatorType; }
            set
            {
                if (!value.IsSubclassOf(typeof(Emulator)))
                    throw new Exception("给定类型不属于Emulator子类");
                emulatorType = value;
            }
        }

#if DEBUG
        public string ResourceRootDirectory { get; set; } = "../../../res";
#else
        public string ResourceRootDirectory { get; set; } = "./res";
#endif

        public string GameKey { get; set; } = "PCR";

        public Region Region { get; set; } = Region.Taiwan;
        
    }

    public enum Region
    {
        Mainland,
        Taiwan,
        Japan,
    }

    public abstract class ConfigMgr
    {
        private static ConfigMgr instance;

        public static ConfigMgr GetInstance()
        {
            instance = instance ?? new JsonConfigMgr();
            return instance;
        }

        public static Config GetConfig()
        {
            return GetInstance().Config;
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
            var config = JsonUtils.DeserializeObject<Config>(content);
            return config;
        }

        public override void SaveConfig()
        {
            var content = JsonUtils.SerializeObject(Config);
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

            var emulatorItems = new ToolStripMenuItem();
            emulatorItems.Text = "模拟器";
            settingItems.DropDownItems.Add(emulatorItems);
            var emulatorMap = new Dictionary<string, Type>();
            var refreshEmulatorCheckStatus = new Action(() =>
            {
                foreach (ToolStripMenuItem item in emulatorItems.DropDownItems)
                {
                    var emulatorType = emulatorMap.Get(item.Text);
                    var bChecked = emulatorType == config.EmulatorType;
                    item.Checked = bChecked;
                    emulatorItems.Text = bChecked ? $"模拟器: {item.Text}" : emulatorItems.Text;
                }
            });
            foreach (var emulatorType in Emulator.GetEmulatorTypes())
            {
                var name = Emulator.GetInstanceByType(emulatorType).Name;
                var emulatorItem = new ToolStripMenuItem();
                emulatorItem.Text = name;
                emulatorMap[name] = emulatorType;
                emulatorItems.DropDownItems.Add(emulatorItem);
                emulatorItem.Click += (s, e) => {
                    config.EmulatorType = emulatorType;
                    ConfigMgr.GetInstance().SaveConfig();
                    refreshEmulatorCheckStatus();
                    EventMgr.FireEvent(EventKeys.ConfigEmulatorTypeChanged);
                };
            }
            refreshEmulatorCheckStatus();

            var regionItems = new ToolStripMenuItem();
            regionItems.Text = "区域";
            settingItems.DropDownItems.Add(regionItems);
            var refreshRegionCheckStatus = new Action(() =>
            {
                foreach (ToolStripMenuItem item in regionItems.DropDownItems)
                {
                    var bChecked = item.Text == config.Region.ToString();
                    item.Checked = bChecked;
                    regionItems.Text = bChecked ? $"区域: {item.Text}" : regionItems.Text;
                }
            });
            foreach (var name in Enum.GetNames(typeof(Region)))
            {
                var regionItem = new ToolStripMenuItem();
                regionItem.Text = name;
                regionItems.DropDownItems.Add(regionItem);
                regionItem.Click += (s, e) => {
                    config.Region = (Region) Enum.Parse(typeof(Region), regionItem.Text);
                    ConfigMgr.GetInstance().SaveConfig();
                    refreshRegionCheckStatus();
                    EventMgr.FireEvent(EventKeys.ConfigRegionChanged);
                };
            }
            refreshRegionCheckStatus();

#if DEBUG
            var openDirItem = new ToolStripMenuItem();
            openDirItem.Text = "打开工作目录";
            menuStrip.Items.Add(openDirItem);
            openDirItem.Click += (s, e) =>
            {
                Utils.OpenDirectoryInExplorer(Environment.CurrentDirectory);
            };
#endif

            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
        }
    }
}
