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

        public CancellationToken CancelToken
        {
            get { return _cancellationTokenSource.Token; }
        }

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

        public ConsoleHelper(Func<IConsole, IColorPreserver> colorPreserverFactory, Func<IConsole, ICursorPreserver> cursorPreserverFactory)
        {
            _cursorPreserverFactory = cursorPreserverFactory;
            _colorPreserverFactory = colorPreserverFactory;
            _cancellationTokenSource = new CancellationTokenSource();

            //set up the cancel keypress to flag the token
            System.Console.CancelKeyPress += (s, e) =>
            {
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

        #region Write

        public void Write(string message)
        {
            Write(message, Foreground, Background);
        }

        public void Write(string message, ConsoleColor foreground)
        {
            Write(message, foreground, Background);
        }

        public void Write(string message, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            using (PreserveColor())
            {
                Foreground = foregroundColor;
                Background = backgroundColor;

                System.Console.Write(message);
            }
        }

        #endregion

        #region WriteLine

        public void WriteLine()
        {
            Write(string.Empty, Foreground, Background);
        }

        public void WriteLine(string message)
        {
            WriteLine(message, Foreground, Background);
        }

        /// <summary>
        /// Writes a message to the console followed by a newline
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <param name="foreground">The foreground color to use</param>
        public void WriteLine(string message, ConsoleColor foreground)
        {
            WriteLine(message, foreground, Background);
        }

        public void WriteLine(string message, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            using (PreserveColor())
            {
                Foreground = foregroundColor;
                Background = backgroundColor;
                System.Console.WriteLine(message);
            }
        }

        #endregion

        #region ReadLine

        public string ReadLine(bool mask = false)
        {
            return ReadLine(string.Empty, Foreground, Background, mask);
        }

        public string ReadLine(string prompt, bool mask = false)
        {
            return ReadLine(prompt, Foreground, Background, mask);
        }

        public string ReadLine(string prompt, ConsoleColor foreground, bool mask = false)
        {
            return ReadLine(prompt, foreground, Background, mask);
        }

        public string ReadLine(string prompt, ConsoleColor foreground, ConsoleColor background, bool mask = false)
        {
            if (string.IsNullOrWhiteSpace(prompt) == false)
            {
                using (PreserveColor())
                {
                    Foreground = foreground;
                    Background = background;
                    Write($"{prompt}: ");
                }
            }

            if (mask)
            { return ReadMasked(); }

            return System.Console.ReadLine();
        }

        private string ReadMasked()
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

        #endregion
    }
}