using System;
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


        public string GetResourcePath(string gameKey, string region, string resourceName)
        {
            var sep = Path.DirectorySeparatorChar;
            var path = $"{RootDirectory}{sep}{gameKey}{sep}{region}{sep}{resourceName}";
            var dir = Path.GetDirectoryName(path);
            Utils.MakeDirectory(dir);
            return path;
        }

        public string GetResourcePath(string gameKey, string region, ResourceType resourceType, string resourceName)
        {
            resourceName = $"{resourceType}{Path.DirectorySeparatorChar}{resourceName}";
            return GetResourcePath(gameKey, region, resourceName);
        }

        public string GetResourcePath(ResourceType resourceType, string resourceName)
        {
            return GetResourcePath(ConfigMgr.GetConfig().GameKey, ConfigMgr.GetConfig().Region.ToString(), resourceType, resourceName);
        }

        public string GetResourcePath(string gameKey, string region, EmulatorSize resolution, string resourceName)
        {
            AspectRatio.AssertResolutionIsSupported(resolution);
            var aspectRatio = AspectRatio.GetAspectRatio(resolution);
            resourceName = $"{aspectRatio}{Path.DirectorySeparatorChar}{resourceName}";
            return GetResourcePath(gameKey, region, resourceName);
        }

        public string GetResourcePath(EmulatorSize resolution, ResourceType resourceType, string resourceName)
        {
            resourceName = $"{resourceType}{Path.DirectorySeparatorChar}{resourceName}";
            return GetResourcePath(resolution, resourceName);
        }

        public string GetResourcePath(EmulatorSize resolution, string resourceName)
        {
            return GetResourcePath(ConfigMgr.GetConfig().GameKey, ConfigMgr.GetConfig().Region.ToString(), resolution, resourceName);
        }

    }

    public enum ResourceType
    {
        Csv,
        Image,
        Json,
    }
}
