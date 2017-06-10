using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using RestAbstractions;
using System;

namespace RestAbstractions.Json
{
    public class JsonResponseDeserializer : IResponseDeserializer
    {
        private readonly JsonSerializerSettings _settings;

        public JsonResponseDeserializer()
        {
            _settings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
        }
        
        public async Task<RestResponse<T>> DeserializeAsync<T>(HttpResponseMessage rawResponse)
        {
            RestResponse<T> response;

            var content = await rawResponse.Content.ReadAsStringAsync();

            T data = default(T);

            try
            {
                data = JsonConvert.DeserializeObject<T>(content, _settings);
            }catch(Exception ex)
            { }
            finally
            {
                response = new RestResponse<T>(data, content, rawResponse);
            }

            return response;
        }
    }
}
