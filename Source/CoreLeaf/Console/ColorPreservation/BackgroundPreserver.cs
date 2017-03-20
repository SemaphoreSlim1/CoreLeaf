using System;

namespace CoreLeaf.Console.ColorPreservation
{
    public class BackgroundPreserver : IDisposable
    {
        private IConsole _console;

        public ConsoleColor Background { get; private set; }

        public BackgroundPreserver(IConsole console)
        {
            _console = console;
            Background = _console.Background;
        }
        public void Dispose()
        {
            _console.Background = Background;
        }
    }
}
