using RestAbstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLeaf.NissanApi.Initial
{
    public class InitialAppClient : NissanClient, IInitialAppClient
    {
        private string _initialAppRoute;

        public InitialAppClient(IRestClient client, string initialAppRoute, string apiKey)
            : base(client, apiKey)
        {
            _initialAppRoute = initialAppRoute;
        }

        public async Task<string> GetEncryptionTokenAsync(CancellationToken cancelToken)
        {            
            var bodyArgs = new List<KeyValuePair<string, string>>();
            bodyArgs.Add(new KeyValuePair<string, string>("cartype", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("custom_sessionid", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("initial_app_strings", _apiKey));
            bodyArgs.Add(new KeyValuePair<string, string>("tz", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("lg", "en-US"));
            bodyArgs.Add(new KeyValuePair<string, string>("DCMID", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("VIN", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("RegionCode", "NNA"));
            
            var response = await PostAsync<InitialAppResponse>(_initialAppRoute, bodyArgs, cancelToken);

            return response.EncryptionKey;
        }
    }
}
