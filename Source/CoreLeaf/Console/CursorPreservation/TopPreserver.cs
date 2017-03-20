using System;

namespace CoreLeaf.Console.CursorPreservation
{
    public class TopPreserver : IDisposable
    {
        private IConsole _console;

        public int Top { get; private set; }

        public TopPreserver(IConsole console)
        {
            _console = console;
            Top = _console.CursorTop;
        }

        public void Dispose()
        {
            _console.CursorTop = Top;
        }
    }
}
