using CoreLeaf.Console.ColorPreservation;
using CoreLeaf.Console.CursorPreservation;
using System;
using System.Text;
using System.Threading;

namespace CoreLeaf.Console
{
    public class ConsoleHelper : IConsole
    {
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

        public ConsoleHelper()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            //set up the cancel keypress to flag the token
            System.Console.CancelKeyPress += (s, e) =>
            {
                _cancellationTokenSource.Cancel();
                e.Cancel = true;
            };
        }
        #region Preserve cursor

        public IDisposable PreserveCursor()
        {
            return new TopLeftPreserver(this);
        }

        /// <summary>
        /// Preserves only the top position of the cursor. To restore, dispose the returned object
        /// </summary>
        /// <returns>The preserved position</returns>
        public IDisposable PreserveCursorTop()
        {
            return new TopPreserver(this);

        }

        /// <summary>
        /// Preserves only the left position of the cursor. To restore, dispose the returned object
        /// </summary>
        /// <returns>The preserved position</returns>
        public IDisposable PreserveCursorLeft()
        {
            return new LeftPreserver(this);
        }

        #endregion

        #region Preserve Color

        public IDisposable PreserveColor()
        {
            return new ForegroundBackgroundPreserver(this);
        }

        public IDisposable PreserveForeground()
        {
            return new ForegroundPreserver(this);
        }

        public IDisposable PreserveBackground()
        {
            return new BackgroundPreserver(this);
        }

        #endregion

        #region clear line

        public void ClearLine(bool setLeftToZero = true)
        {
            ClearLine(CursorTop, setLeftToZero);
        }

        public void ClearLine(int lineNumber, bool setLeftToZero = true)
        {
            using (PreserveCursor())
            {
                CursorLeft = 0;
                CursorTop = lineNumber;
                Write(new string(' ', System.Console.WindowWidth));
            }

            if (setLeftToZero)
            {
                CursorLeft = 0;
            }
        }

        #endregion

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
            WriteLine(string.Empty, Foreground, Background);
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