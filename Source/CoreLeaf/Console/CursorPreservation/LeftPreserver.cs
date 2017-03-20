using System;

namespace CoreLeaf.Console.CursorPreservation
{
    public class LeftPreserver : IDisposable
    {
        private IConsole _console;

        public int Left { get; private set; }

        public LeftPreserver(IConsole console)
        {
            _console = console;
            Left = _console.CursorLeft;
        }

        public void Dispose()
        {
            _console.CursorTop = Left;
        }
    }
}
