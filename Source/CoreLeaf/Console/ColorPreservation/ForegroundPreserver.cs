using System;

namespace CoreLeaf.Console.ColorPreservation
{
    public class ForegroundPreserver : IDisposable
    {
        private IConsole _console;

        public ConsoleColor Foreground { get; private set; }

        public ForegroundPreserver(IConsole console)
        {
            _console = console;
            Foreground = _console.Foreground;
        }

        public void Dispose()
        {
            _console.Foreground = Foreground;
        }
    }
}
