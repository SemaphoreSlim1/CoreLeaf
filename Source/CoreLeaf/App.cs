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
            using (_console.PreserveColor())
            using (_console.PreserveCursorPosition())
            {
                _console.CursorLeft += 5;
                _console.CursorTop += 1;
                _console.WriteLine("Hello World!", ConsoleColor.Green);
            }
        }
    }
}
