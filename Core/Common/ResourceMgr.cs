using System;
using System.Collections.Generic;
using System.IO;

using Core.Emulators;
using Core.Extensions;
using System.Text.RegularExpressions;

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

        public static string Separator { get { return $"{Path.DirectorySeparatorChar}"; } }

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

        private bool useGameKeyAndRegion = true;

        public string GameKey { get; set; }

        public string Region { get; set; }

        private bool useAspectRatio = false;

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
            if (useGameKeyAndRegion && (string.IsNullOrWhiteSpace(GameKey) || string.IsNullOrWhiteSpace(Region)))
                throw new Exception("GameKey和Region不能为空");
            if (useAspectRatio && AspectRatio == null)
                throw new Exception("AspectRatio不能为空");
        }

        private List<string> pieces = new List<string>();

        private List<string> GetPathPieces(params string[] otherPieces)
        {
            var r = new List<string>() { RootDirectory };
            if (useGameKeyAndRegion)
                r.AddRange(new List<string>() { GameKey, Region });
            if (useAspectRatio)
                r.Add(AspectRatio.ToString());
            foreach (var piece in pieces)
                r.Add(piece);
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

        public static readonly string CSV_PIECE = "Csv";

        public Resource Csv(string resourceName)
        {
            var fullPath = GetFullPath(CSV_PIECE, resourceName);
            return new Resource(fullPath);
        }

        public static readonly string IMAGE_PIECE = "Image";

        public Resource Image(string resourceName)
        {
            var fullPath = GetFullPath(IMAGE_PIECE, resourceName);
            return new Resource(fullPath);
        }

        public static readonly string JSON_PIECE = "Json";

        public Resource Json(string resourceName)
        {
            var fullPath = GetFullPath(JSON_PIECE, resourceName);
            return new Resource(fullPath);
        }

        public ResourceMgr Clone()
        {
            var r = new ResourceMgr(RootDirectory)
            {
                GameKey = GameKey,
                Region = Region,
                AspectRatio = AspectRatio,

                useGameKeyAndRegion = useGameKeyAndRegion,
                useAspectRatio = useAspectRatio,

                pieces = new List<string>(pieces),
            };
            return r;
        }

        public ResourceMgr WithAspectRatio()
        {
            var r = Clone();
            r.useAspectRatio = true;
            return r;
        }

        public ResourceMgr WithPieces(IEnumerable<string> pieces)
        {
            var r = Clone();
            r.pieces.AddRange(pieces);
            return r;
        }
    }


    public class ResourceManager
    {
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

        private string GetEscapeString(string str)
        {

            //var matches = Regex.Replace(str, "$\\(.+?){\\}", );
            return null;
        }

        public string GetFullPath(string path)
        {
            return path;
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
