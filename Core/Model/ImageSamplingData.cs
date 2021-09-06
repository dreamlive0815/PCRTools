using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using Core.Common;
using Core.Emulators;
using Newtonsoft.Json;

namespace Core.Model
{
    public class ImageSamplingData
    {


        public static ImageSamplingData GetByPath(string path)
        {
            var res = ResourceManager.Default.GetResource($"{path}/Json/" + DefaultFileName);
            var data = res.ParseObjWithCache(int.MaxValue, (filePath) =>
            {
                return FromFile(filePath);
            });
            data.Path = path;
            return data;
        }

        public static ImageSamplingData GetCommon()
        {
            return GetByPath("${G}");
        }

        public static ImageSamplingData GetWithAspectRatio()
        {
            return GetByPath("${G}/${R}/${A}");
        }

        public static string DefaultFileName { get { return "image_sampling_data.json"; } }

        public static ImageSamplingData FromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return new ImageSamplingData();
            var s = File.ReadAllText(filePath);
            var r = JsonUtils.DeserializeObject<ImageSamplingData>(s);
            return r;
        }

        [JsonIgnore]
        public string Path { get; set; }

        public ImageSamplingDataContainer<Size> ContainerSizes { get; set; } = new ImageSamplingDataContainer<Size>();

        /// <summary>
        /// 百分比
        /// </summary>
        public ImageSamplingDataContainer<int> MatchThresholds { get; set; } = new ImageSamplingDataContainer<int>();

        public ImageSamplingDataContainer<PVec2f> PVec2fs { get; set; } = new ImageSamplingDataContainer<PVec2f>();

        public ImageSamplingDataContainer<RVec2f> RVec2fs { get; set; } = new ImageSamplingDataContainer<RVec2f>();

        public ImageSamplingDataContainer<RVec4f> RVec4fs { get; set; } = new ImageSamplingDataContainer<RVec4f>();

        public double GetThreshold(string key)
        {
            if (!MatchThresholds.ContainsKey(key))
                throw new Exception($"找不到MatchThreshold: {key}");
            return 0.01 * MatchThresholds[key];
        }

        public Size GetContainerSize(string key)
        {
            if (!ContainerSizes.ContainsKey(key))
                throw new Exception($"找不到ContainerSize: {key}");
            return ContainerSizes[key];
        }

        public RVec4f GetRectVec4f(string key)
        {
            if (!RVec4fs.ContainsKey(key))
                throw new Exception($"找不到RectVec4f: {key}");
            return RVec4fs[key];
        }

        public void Save(string filePath)
        {
            var s = JsonUtils.SerializeObject(this);
            File.WriteAllText(filePath, s);
        }

        public Img GetResizedImg(string key, Size emulatorRectSize)
        {
            var containerSize = GetContainerSize(key);
            var fullPath = ResourceManager.Default.GetFullPath($"{Path}/Image/" + key);
            var img = new Img(fullPath);
            var scale = 1.0 * emulatorRectSize.Width / containerSize.Width;
            img = img.GetScaled(scale);
            return img;
        }
    }

    public class ImageSamplingDataContainer<T> : SortedDictionary<string, T>
    {

        public void Set(string key, T value)
        {
            this[key] = value;
        }
    }
}
