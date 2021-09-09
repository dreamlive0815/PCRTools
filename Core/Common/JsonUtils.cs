using System;
using System.Collections.Generic;
using System.Drawing;

using Core.Emulators;

using Newtonsoft.Json;

namespace Core.Common
{
    public class JsonUtils
    {

        private static JsonSerializerSettings setting = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter>()
                {
                    new PVec2fConverter(), new RVec2fConverter(), new RVec4fConverter(),
                    new SizeConverter(), new RectangleConverter(),
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


    public class RVec2fConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(RVec2f);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var s = reader.Value.ToString();
            return RVec2f.Parse(s);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }


    public class RVec4fConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(RVec4f);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var s = reader.Value.ToString();
            return RVec4f.Parse(s);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }

    public class SizeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Size);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var s = reader.Value.ToString();
            var arr = s.Split(',');
            return new Size(int.Parse(arr[0]), int.Parse(arr[1]));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var size = (Size)value;
            var s = $"{size.Width},{size.Height}";
            writer.WriteValue(s);
        }
    }


    public class RectangleConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Rectangle);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var s = reader.Value.ToString();
            var arr = s.Split(',');
            return new Rectangle(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]), int.Parse(arr[3]));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var rect = (Rectangle)value;
            var s = $"{rect.X},{rect.Y},{rect.Width},{rect.Height}";
            writer.WriteValue(s);
        }
    }
}
