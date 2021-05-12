using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using Core.Common;
using Core.Emulators;

namespace Core.Model
{
    public class Vecs
    {
        public static Vecs Parse(string filePath)
        {
            if (!File.Exists(filePath))
                return new Vecs();
            var r = JsonUtils.DeserializeObject<Vecs>(filePath);
            return r;
        }

        public Dictionary<string, Size> ContainerSize { get; set; } = new Dictionary<string, Size>();

        public Dictionary<string, PVec2f> PVec2fs { get; set; } = new Dictionary<string, PVec2f>();

        public Dictionary<string, RVec4f> RVec4fs { get; set; } = new Dictionary<string, RVec4f>();

        public void Save(string filePath)
        {
            var s = JsonUtils.SerializeObject(this);
            File.WriteAllText(filePath, s);
        }
    }
}
