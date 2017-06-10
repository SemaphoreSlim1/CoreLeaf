using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLeaf.NissanApi.Login
{
    public class VehicleInfoListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jobj = JObject.Load(reader);
            var props = jobj.Properties();

            var infos = new List<VehicleInfo>();
            //each property in this list is an array of 1 vehicle info
            foreach (var prop in props)
            {
                var vehicleInfo = prop.Value.First.ToObject<VehicleInfo>();
                infos.Add(vehicleInfo);
            }

            return infos;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
