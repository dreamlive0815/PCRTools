using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using Core.Common;
using Core.Emulators;

namespace Core.Model
{
    public class ImageSamplingData
    {

        public static string DefaultFileName { get { return "image_sampling_data.json"; } }

        public static ImageSamplingData FromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return new ImageSamplingData();
            var s = File.ReadAllText(filePath);
            var r = JsonUtils.DeserializeObject<ImageSamplingData>(s);
            return r;
        }

        public ImageSamplingDataContainer<Size> ContainerSizes { get; set; } = new ImageSamplingDataContainer<Size>();

        /// <summary>
        /// 百分比
        /// </summary>
        public ImageSamplingDataContainer<int> MatchThresholds { get; set; } = new ImageSamplingDataContainer<int>();

        public ImageSamplingDataContainer<PVec2f> PVec2fs { get; set; } = new ImageSamplingDataContainer<PVec2f>();

        public ImageSamplingDataContainer<RVec2f> RVec2fs { get; set; } = new ImageSamplingDataContainer<RVec2f>();

        public ImageSamplingDataContainer<RVec4f> RVec4fs { get; set; } = new ImageSamplingDataContainer<RVec4f>();

        public void Save(string filePath)
        {
            var s = JsonUtils.SerializeObject(this);
            File.WriteAllText(filePath, s);
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
