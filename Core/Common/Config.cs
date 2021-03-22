using System;
using System.IO;

using Newtonsoft.Json;

using Core.Emulators;
using Core.PCR;

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
}
