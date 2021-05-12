using Core.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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

        public Dictionary<string, Size> ContainerSize = new Dictionary<string, Size>();


        public void Save(string filePath)
        {
            var s = JsonUtils.SerializeObject(this);
            File.WriteAllText(filePath, s);
        }
    }
}
