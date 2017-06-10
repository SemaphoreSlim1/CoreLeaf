using ConsoleAbstractions;
using CoreLeaf.Configuration;
using CoreLeaf.NissanApi.Countries;
using CoreLeaf.NissanApi.Initial;
using CoreLeaf.NissanApi.Login;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using System.Linq;
using LeafStandard.NissanApi.Status;

namespace CoreLeaf
{
    public class App
    {
        private readonly IConfiguration _config;
        private readonly IConsole _console;
        private readonly ICountryClient _countryClient;
        private readonly IInitialAppClient _initialAppClient;
        private readonly ILoginClient _loginClient;
        private readonly IBatteryStatusClient _batteryStatusClient;

        public App(IConfiguration config, IConsole console, 
            ICountryClient countryClient, 
            IInitialAppClient initialAppClient,
            ILoginClient loginClient,
            IBatteryStatusClient batteryStatusClient)
        {
            _config = config;
            _console = console;
            _countryClient = countryClient;
            _initialAppClient = initialAppClient;
            _loginClient = loginClient;
            _batteryStatusClient = batteryStatusClient;
        }

        public async Task Run(string[] args)
        {            
           
            var settings = await _countryClient.GetSettingsAsync(_console.CancelToken);

            var encryptionToken = await _initialAppClient.GetEncryptionTokenAsync(_console.CancelToken);

            var userName = _config[ConfigurationKeys.UserName];
            var password = _config[ConfigurationKeys.Password];

            var result = await _loginClient.LoginAsync(encryptionToken, userName, password, _console.CancelToken);

            var sessionId = result.VehicleInfos.Select(vi => vi.SessionId).First(x => string.IsNullOrWhiteSpace(x) == false);

            var batteryStatus = await _batteryStatusClient.GetStatusAsync(sessionId, _console.CancelToken);
            


           // _console.WriteLine(result.ToString());
           // _console.ReadLine();
        }
    }
}
