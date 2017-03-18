using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLeaf.Console
{
    public delegate ICursorPreserver CursorPreserverFactory(IConsole console);

    public class CursorPreserver : ICursorPreserver
    {
        public int Top { get; private set; }

        public int Left { get; private set; }

        private IConsole _console;

        public CursorPreserver(IConsole console)
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
