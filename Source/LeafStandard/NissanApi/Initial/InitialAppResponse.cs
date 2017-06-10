using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLeaf.NissanApi.Initial
{
    public class InitialAppResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("baseprm")]
        public string EncryptionKey { get; set; }
    }
}
