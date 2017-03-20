using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreLeaf.Net
{
    public class JsonResponseDeserializer : IResponseDeserializer
    {
        private readonly JsonSerializerSettings _settings;

        public JsonResponseDeserializer()
        {
            _settings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
        }
        
        public async Task<TResponse> DeserializeAsync<TResponse>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(responseString, _settings);
        }
    }
}
