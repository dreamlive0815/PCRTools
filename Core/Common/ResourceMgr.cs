using System;
using System.Collections.Generic;
using System.IO;

using Core.Emulators;
using Core.Extensions;

namespace Core.Common
{

    public class ResourceMgr
    {

        private static ResourceMgr instance;
        private static ResourceMgr instanceUseAspectRatio;

        public static ResourceMgr Default
        {
            get
            {
                if (instance == null)
                {
                    var config = ConfigMgr.GetConfig();
                    instance = new ResourceMgr(config.ResourceRootDirectory)
                    {
                        GameKey = config.GameKey,
                        Region = config.Region.ToString(),
                    };
                }
                return instance;
            }
        }

        public static ResourceMgr DefaultUseAspectRatio
        {
            get
            {
                if (instanceUseAspectRatio == null)
                {
                    instanceUseAspectRatio = Default.WithAspectRatio();
                }
                return instanceUseAspectRatio;
            }
        }

        public ResourceMgr(string rootDirectory)
        {
#if !DEBUG

            if (!Directory.Exists(rootDirectory))
                throw new Exception($"资源目录不存在: {rootDirectory}");
#endif
            Utils.MakeDirectory(rootDirectory);
            RootDirectory = rootDirectory;
            
        }

        public string RootDirectory { get; private set; }

        private bool UseGameKeyAndRegion = true;

        public string GameKey { get; set; }

        public string Region { get; set; }

        private bool UseAspectRatio = false;

        public AspectRatio AspectRatio { get; private set; }

        public ResourceMgr SetAspectRatioByResolution(EmulatorSize resolution)
        {
            AspectRatio.AssertResolutionIsSupported(resolution);
            var aspectRatio = AspectRatio.GetAspectRatio(resolution);
            AspectRatio = aspectRatio;
            return this;
        }

        private void AssertFieldsNotEmpty()
        {
            if (UseGameKeyAndRegion && (string.IsNullOrWhiteSpace(GameKey) || string.IsNullOrWhiteSpace(Region)))
                throw new Exception("GameKey和Region不能为空");
            if (UseAspectRatio && AspectRatio == null)
                throw new Exception("AspectRatio不能为空");
        }

        private List<string> GetPathPieces(params string[] otherPieces)
        {
            var r = new List<string>() { RootDirectory };
            if (UseGameKeyAndRegion)
                r.AddRange(new List<string>() { GameKey, Region });
            if (UseAspectRatio)
                r.Add(AspectRatio.ToString());
            foreach (var piece in otherPieces)
                r.Add(piece);
            return r;
        }

        public string GetFullPath(params string[] pieces)
        {
            AssertFieldsNotEmpty();
            var arr = GetPathPieces(pieces);
            var path = string.Join($"{Path.DirectorySeparatorChar}", arr);
            path = Path.GetFullPath(path);

            var dir = Path.GetDirectoryName(path);
            Utils.MakeDirectory(dir);

            return path;
        }

        public Resource GetResource(params string[] pieces)
        {
            var fullPath = GetFullPath(pieces);
            return new Resource(fullPath);
        }

        public Resource Csv(string resourceName)
        {
            var fullPath = GetFullPath("Csv", resourceName);
            return new Resource(fullPath);
        }

        public Resource Image(string resourceName)
        {
            var fullPath = GetFullPath("Image", resourceName);
            return new Resource(fullPath);
        }

        public Resource Json(string resourceName)
        {
            var fullPath = GetFullPath("Json", resourceName);
            return new Resource(fullPath);
        }

        public ResourceMgr Clone()
        {
            var r = new ResourceMgr(RootDirectory)
            {
                GameKey = GameKey,
                Region = Region,
                AspectRatio = AspectRatio,
            };
            return r;
        }

        public ResourceMgr WithAspectRatio()
        {
            var r = Clone();
            r.UseAspectRatio = true;
            return r;
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
