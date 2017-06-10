using LeafStandard.NissanApi;
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
            var args = new BodyArgs();
            args.Add("cartype");
            args.Add("custom_sessionid");
            args.Add("initial_app_strings", _apiKey);
            args.Add("tz");
            args.Add("lg", "en-US");
            args.Add("DCMID");
            args.Add("VIN");
            args.Add("RegionCode", "NNA");
            
            var response = await PostAsync<InitialAppResponse>(_initialAppRoute, args, cancelToken);

            return response.EncryptionKey;
        }
    }
}
