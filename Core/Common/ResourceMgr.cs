using System;
using System.Collections.Generic;
using System.IO;

using Core.Emulators;
using Core.Extensions;
using System.Text.RegularExpressions;

namespace Core.Common
{

    public class ResourceManager
    {

        private static ResourceManager instance;

        public static ResourceManager Default
        {
            get
            {
                if (instance == null)
                {
                    var config = ConfigMgr.GetConfig();
                    instance = new ResourceManager(config.ResourceRootDirectory)
                    {
                        GameKey = config.GameKey,
                        Region = config.Region.ToString(),
                    };
                }
                return instance;
            }
        }

        public ResourceManager(string rootDirectory)
        {
#if !DEBUG

            if (!Directory.Exists(rootDirectory))
                throw new Exception($"资源目录不存在: {rootDirectory}");
#endif
            Utils.MakeDirectory(rootDirectory);
            RootDirectory = rootDirectory;

        }

        public string RootDirectory { get; private set; }

        public string GameKey { get; set; }

        public string Region { get; set; }

        public AspectRatio AspectRatio { get; private set; }

        public ResourceManager SetAspectRatioByResolution(EmulatorSize resolution)
        {
            AspectRatio.AssertResolutionIsSupported(resolution);
            var aspectRatio = AspectRatio.GetAspectRatio(resolution);
            AspectRatio = aspectRatio;
            return this;
        }

        private Dictionary<string, string> shortPropertyKeys = new Dictionary<string, string>()
        {
            { "G", "GameKey" },
            { "R", "Region" },
            { "A", "AspectRatio" },
        };

        private string GetPropertyValue(string key)
        {
            if (shortPropertyKeys.ContainsKey(key))
                key = shortPropertyKeys[key];
            var type = typeof(ResourceManager);
            var property = type.GetProperty(key);
            if (property == null)
                throw new Exception($"属性:{key}不存在");
            var val = property.GetValue(this);
            if (val == null)
                throw new Exception($"属性:{key}值为空");
            return val.ToString();
        }

        private string GetEscapeString(string str)
        {
            var replaced = Regex.Replace(str, "\\$\\{(.+?)\\}", (match) =>
            {
                return GetPropertyValue(match.Groups[1].Value);
            });
            return replaced;
        }

        public string GetFullPath(string path)
        {
            path = GetEscapeString(path);
            path = $"{RootDirectory}/{path}";
            path = Path.GetFullPath(path);

            var dir = Path.GetDirectoryName(path);
            Utils.MakeDirectory(dir);

            return path;
        }

        public Resource GetResource(string path)
        {
            var fullPath = GetFullPath(path);
            return new Resource(fullPath);
        }
    }


    public class Resource
    {
        public Resource(string fullPath)
        {
            Fullpath = fullPath;
        }

        public string Fullpath { get; private set; }

        public bool Exists
        {
            get { return File.Exists(Fullpath); }
        }

        public Resource AssertExists()
        {
            if (!Exists)
                throw new Exception($"资源不存在 {Fullpath}");
            return this;
        }
    }
}
