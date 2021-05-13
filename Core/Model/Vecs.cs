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
            var s = File.ReadAllText(filePath);
            var r = JsonUtils.DeserializeObject<Vecs>(s);
            return r;
        }

        public KVContainer<Size> ContainerSizes { get; set; } = new KVContainer<Size>();

        public KVContainer<PVec2f> PVec2fs { get; set; } = new KVContainer<PVec2f>();

        public KVContainer<RVec4f> RVec4fs { get; set; } = new KVContainer<RVec4f>();

        public void Save(string filePath)
        {
            var s = JsonUtils.SerializeObject(this);
            File.WriteAllText(filePath, s);
        }
    }

    public class KVContainer<T> : SortedDictionary<string, T>
    {

        public void Set(string key, T value)
        {
            if (ContainsKey(key))
                this[key] = value;
            else
                Add(key, value);
        }
    }
}
