namespace CoreLeaf.Console
{
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