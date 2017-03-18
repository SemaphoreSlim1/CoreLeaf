using System;

namespace CoreLeaf.Console
{
    public class ColorPreserver : IColorPreserver
    {
        public ConsoleColor Background { get; private set; }

        public ConsoleColor Foreground { get; private set; }

        private IConsole _console;

        public ColorPreserver(IConsole console)
        {
            _console = console;
            Background = _console.Background;
            Foreground = _console.Foreground;
        }

        public void Dispose()
        {
            _console.Background = Background;
            _console.Foreground = Foreground;
            _console = null;
        }
    }
}