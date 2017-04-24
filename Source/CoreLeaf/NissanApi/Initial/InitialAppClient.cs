using CoreLeaf.Configuration;
using CoreLeaf.Net;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLeaf.NissanApi.Initial
{
    public class InitialAppClient : NissanClient, IInitialAppClient
    {
        public InitialAppClient(IConfiguration config, IRestClient client)
            : base(config, client)
        {
        }

        public async Task<string> GetEncryptionTokenAsync(CancellationToken cancelToken)
        {
            var route = _config[ConfigurationKeys.InitialAppRoute];
            var apiKey = _config[ConfigurationKeys.NissanApiKey];

            var bodyArgs = new List<KeyValuePair<string, string>>();
            bodyArgs.Add(new KeyValuePair<string, string>("cartype", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("custom_sessionid", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("initial_app_strings", apiKey));
            bodyArgs.Add(new KeyValuePair<string, string>("tz", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("lg", "en-US"));
            bodyArgs.Add(new KeyValuePair<string, string>("DCMID", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("VIN", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("RegionCode", "NNA"));
            
            var response = await PostAsync<InitialAppResponse>(route, bodyArgs, cancelToken);

            return response.EncryptionKey;
        }
    }
}
