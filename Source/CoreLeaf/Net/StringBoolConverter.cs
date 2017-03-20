using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace CoreLeaf.Net
{
    public class StringBoolConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(IDictionary<string, bool>))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobj = JObject.Load(reader);
            var props = jobj.Properties();

            var dict = new Dictionary<string, bool>();

            foreach (var prop in props)
            {
                var intVal = Convert.ToInt32(prop.Value.Value<string>());
                var boolVal = Convert.ToBoolean(intVal);
                dict.Add(prop.Name, boolVal);
            }

            return dict;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
