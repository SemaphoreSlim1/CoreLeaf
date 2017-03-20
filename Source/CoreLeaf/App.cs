using CoreLeaf.Console;
using System;
using System.Threading.Tasks;

namespace CoreLeaf
{
    public class App
    {
        private IConsole _console;
        public App(IConsole console)
        {
            _console = console;
        }

        public async Task Run(string[] args)
        {
            var initialAppString = _console.ReadLine("Initial App String", ConsoleColor.Yellow);
            
        }
    }
}
