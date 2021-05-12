﻿using System;
using System.IO;

using Core.Emulators;
using Core.Extensions;

namespace Core.Common
{
    public class ResourceMgr
    {
        private static ResourceMgr instance;

        public static ResourceMgr GetInstance()
        {
            return instance = instance ?? new ResourceMgr();
        }

        protected ResourceMgr()
        {
            Utils.MakeDirectory(RootDirectory);
        }

        public string RootDirectory { get { return ConfigMgr.GetConfig().ResourceRootDirectory; } }

        public string GetResourcePath(EmulatorSize resolution, string resourceName)
        {
            return GetResourcePath(ConfigMgr.GetConfig().GameKey, ConfigMgr.GetConfig().Region.ToString(), resolution, resourceName);
        }

        public string GetResourcePath(string gameKey, string region, string resourceName)
        {
            var sep = Path.DirectorySeparatorChar;
            var path = $"{RootDirectory}{sep}{gameKey}{sep}{region}{sep}{resourceName}";
            var dir = Path.GetDirectoryName(path);
            Utils.MakeDirectory(dir);
            return path;
        }

        public string GetResourcePath(string gameKey, string region, EmulatorSize resolution, string resourceName)
        {
            return GetResourcePath(gameKey, region, $"{resolution}{Path.DirectorySeparatorChar}{resourceName}");
        }

    }
}