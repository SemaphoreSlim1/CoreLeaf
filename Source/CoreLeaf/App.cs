using CoreLeaf.Console;
using CoreLeaf.NissanApi.Countries;
using System;
using System.Threading.Tasks;

namespace CoreLeaf
{
    public class App
    {
        private IConsole _console;
        private ICountryClient _countryClient;

        public App(IConsole console, ICountryClient countryClient)
        {
            _console = console;
            _countryClient = countryClient;
        }

        public async Task Run(string[] args)
        {
            var apiKey = _console.ReadLine("Nissan API Key", ConsoleColor.Yellow);
           
            var settings = await _countryClient.GetSettingsAsync(apiKey, _console.CancelToken);                       
        }
    }
}
