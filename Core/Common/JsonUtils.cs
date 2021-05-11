using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Core.Emulators;

using Newtonsoft.Json;

namespace Core.Common
{
    public class JsonUtils
    {

        private static JsonSerializerSettings setting = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            Converters = new List<JsonConverter>()
                {
                    new PVec2fConverter(),
                },
        };

        public static string SerializeObject(object obj)
        {
            var s = JsonConvert.SerializeObject(obj, setting);
            return s;
        }

        public static T DeserializeObject<T>(string jsonStr)
        {
            var r = JsonConvert.DeserializeObject<T>(jsonStr, setting);
            return r;
        }
    }

    public class PVec2fConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PVec2f);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var s = reader.Value.ToString();
            return PVec2f.Parse(s);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
