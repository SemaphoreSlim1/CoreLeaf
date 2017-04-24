using CoreLeaf.Console;
using CoreLeaf.NissanApi.Countries;
using CoreLeaf.NissanApi.Initial;
using CoreLeaf.NissanApi.Login;
using System;
using System.Threading.Tasks;

namespace CoreLeaf
{
    public class App
    {
        private readonly IConsole _console;
        private readonly ICountryClient _countryClient;
        private readonly IInitialAppClient _initialAppClient;
        private readonly ILoginClient _loginClient;

        public App(IConsole console, 
            ICountryClient countryClient, 
            IInitialAppClient initialAppClient,
            ILoginClient loginClient)
        {
            _console = console;
            _countryClient = countryClient;
            _initialAppClient = initialAppClient;
            _loginClient = loginClient;
        }

        public async Task Run(string[] args)
        {            
           
            var settings = await _countryClient.GetSettingsAsync(_console.CancelToken);

            var encryptionToken = await _initialAppClient.GetEncryptionTokenAsync(_console.CancelToken);

            var result = await _loginClient.LoginAsync(encryptionToken, _console.CancelToken);

           // _console.WriteLine(result.ToString());
           // _console.ReadLine();
        }
    }
}
