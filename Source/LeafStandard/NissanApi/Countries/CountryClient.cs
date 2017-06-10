using LeafStandard.NissanApi;
using RestAbstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLeaf.NissanApi.Countries
{
    public class CountryClient : NissanClient, ICountryClient
    {
        private readonly string _countryRoute;
        public CountryClient(IRestClient client, string countryRoute, string apiKey)
            : base(client, apiKey)
        {
            _countryRoute = countryRoute;
        }

        public async Task<IDictionary<string, bool>> GetSettingsAsync(CancellationToken cancelToken)
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

            var response = await PostAsync<CountryResponse>(_countryRoute, args, cancelToken);

            return response.Settings;
        }
    }
}
