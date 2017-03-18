using System;
using System.Text;
using System.Threading;

namespace CoreLeaf.Console
{
    public class ConsoleHelper : IConsole
    {
        private Func<IConsole, ICursorPreserver> _cursorPreserverFactory;
        private Func<IConsole, IColorPreserver> _colorPreserverFactory;
        private CancellationTokenSource _cancellationTokenSource;

        public int CursorTop
        {
            get { return System.Console.CursorTop; }
            set { System.Console.SetCursorPosition(CursorLeft, value); }
        }

        public int CursorLeft
        {
            get { return System.Console.CursorLeft; }
            set { System.Console.SetCursorPosition(value, CursorTop); }
        }

        public ConsoleColor Foreground
        {
            get { return System.Console.ForegroundColor; }
            set { System.Console.ForegroundColor = value; }
        }

        public ConsoleColor Background
        {
            get { return System.Console.BackgroundColor; }
            set { System.Console.BackgroundColor = value; }
        }

        public ConsoleHelper(Func<IConsole,IColorPreserver> colorPreserverFactory, Func<IConsole,ICursorPreserver> cursorPreserverFactory)
        {
            _cursorPreserverFactory = cursorPreserverFactory;
            _colorPreserverFactory = colorPreserverFactory;
            _cancellationTokenSource = new CancellationTokenSource();

            //set up the cancel keypress to flag the token
            System.Console.CancelKeyPress += (s, e) => {
                _cancellationTokenSource.Cancel();
                e.Cancel = true;
            };
        }

        public IDisposable PreserveCursorPosition()
        {
            return _cursorPreserverFactory(this);
        }

        public IDisposable PreserveColor()
        {
            return _colorPreserverFactory(this);
        }

        public CancellationToken GetCancelToken()
        {
            return _cancellationTokenSource.Token;
        }

        public void ClearLine()
        {
            ClearLine(CursorTop);
        }

        public void ClearLine(int lineNumber)
        {
            using (PreserveCursorPosition())
            {
                CursorLeft = 0;
                CursorTop = lineNumber;
                Write(new string(' ', System.Console.WindowWidth));
            }
        }

        public void Write(string message, ConsoleColor foregroundColor = ConsoleColor.Gray)
        {
            using (PreserveColor())
            {
                Foreground = foregroundColor;
                System.Console.Write(message);
            }
        }

        public void WriteLine(string message, ConsoleColor foregroundColor = ConsoleColor.Gray)
        {
            using (PreserveColor())
            {
                Foreground = foregroundColor;
                System.Console.WriteLine(message);
            }
        }

        public string ReadLine()
        {
            return System.Console.ReadLine();
        }

        public string ReadHiddenContent()
        {
            var content = new StringBuilder();

            while (true)
            {
                var i = System.Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    content.Remove(content.Length - 1, 1);
                    Write("\b \b");
                }
                else
                {
                    content.Append(i.KeyChar);
                    Write("*");
                }
            }

            WriteLine(string.Empty);
            return content.ToString();
        }
    }
}