using System;

namespace Core.Common
{
    public class Config
    {
        public Config()
        {

        }
    }

    public class ConfigMgr
    {
        private static ConfigMgr instance;

        public ConfigMgr GetInstance()
        {
            instance = instance ?? new JsonConfigMgr();
            return instance;
        }

        private static Config config;

        //public Config 
    }

    public class JsonConfigMgr : ConfigMgr
    {

    }
}
