using CoreLeaf.Console;
using CoreLeaf.NissanApi.Countries;
using CoreLeaf.NissanApi.Initial;
using System;
using System.Threading.Tasks;

namespace CoreLeaf
{
    public class App
    {
        private readonly IConsole _console;
        private readonly ICountryClient _countryClient;
        private readonly IInitialAppClient _initialAppClient;

        public App(IConsole console, ICountryClient countryClient, IInitialAppClient initialAppClient)
        {
            _console = console;
            _countryClient = countryClient;
            _initialAppClient = initialAppClient;
        }

        public async Task Run(string[] args)
        {
            var apiKey = _console.ReadLine("Nissan API Key", ConsoleColor.Yellow);
           
            var settings = await _countryClient.GetSettingsAsync(apiKey, _console.CancelToken);

            var encryptionToken = await _initialAppClient.GetEncryptionTokenAsync(_console.CancelToken);
        }
    }
}
