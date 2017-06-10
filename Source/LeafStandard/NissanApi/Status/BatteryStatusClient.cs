using CoreLeaf.NissanApi;
using System;
using System.Collections.Generic;
using System.Text;
using RestAbstractions;
using System.Threading.Tasks;
using System.Threading;

namespace LeafStandard.NissanApi.Status
{
    public class BatteryStatusClient : NissanClient, IBatteryStatusClient
    {
        private readonly string _batteryStatusRoute;

        public BatteryStatusClient(IRestClient client, string batteryStatusRoute, string apiKey) 
            : base(client, apiKey)
        {
            _batteryStatusRoute = batteryStatusRoute;
        }

        public Task<BatteryStatusResponse> GetStatusAsync(string sessionId, CancellationToken cancelToken)
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

            var result = this.PostAsync<BatteryStatusResponse>(_batteryStatusRoute, args, cancelToken);

            return result;
        }
    }
}
