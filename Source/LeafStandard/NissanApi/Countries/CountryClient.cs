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
            var bodyArgs = new List<KeyValuePair<string, string>>();

            bodyArgs.Add(new KeyValuePair<string, string>("cartype", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("custom_sessionid", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("initial_app_strings", _apiKey));
            bodyArgs.Add(new KeyValuePair<string, string>("tz", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("lg", "en-US"));
            bodyArgs.Add(new KeyValuePair<string, string>("DCMID", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("VIN", string.Empty));
            bodyArgs.Add(new KeyValuePair<string, string>("RegionCode", "NNA"));

            var response = await PostAsync<CountryResponse>(_countryRoute, bodyArgs, cancelToken);

            return response.Settings;
        }
    }
}
