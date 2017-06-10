using System;

namespace ConsoleAbstractions.CursorPreservation
{
    public class TopLeftPreserver : IDisposable
    {
        private IConsole _console;

        public int Top { get; private set; }

        public int Left { get; private set; }              

        public TopLeftPreserver(IConsole console)
        {
            _console = console;
            Top = _console.CursorTop;
            Left = _console.CursorLeft;
        }

        public void Dispose()
        {
            _console.CursorLeft = Left;
            _console.CursorTop = Top;
            _console = null;
        }
    }
}