using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public string RootDirectory { get { return ConfigMgr.GetInstance().Config.ResourceRootDirectory; } }

        public string GetResourcePath(string type, string region, string resourceName)
        {
            var sep = Path.DirectorySeparatorChar;
            var path = $"{RootDirectory}{sep}{type}{sep}{region}{sep}{resourceName}";
            var dir = Path.GetDirectoryName(path);
            Utils.MakeDirectory(dir);
            return path;
        }

        public string GetResourcePath(string type, string region, EmulatorSize resolution, string resourceName)
        {
            return GetResourcePath(type, region, $"{resolution}{Path.DirectorySeparatorChar}{resourceName}");
        }

        public string SelectResourcePath(string type, string region, EmulatorSize resolution, string resourceName)
        {
            return null;
        }
    }
}
