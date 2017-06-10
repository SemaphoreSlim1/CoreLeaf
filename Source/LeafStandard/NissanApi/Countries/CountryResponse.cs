using RestAbstractions.Json;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoreLeaf.NissanApi.Countries
{
    public class CountryResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("setting")]
        [JsonConverter(typeof(StringBoolConverter))]
        public Dictionary<string, bool> Settings { get; set; }
    }
}
