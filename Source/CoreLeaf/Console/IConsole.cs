using System;
using System.Threading;

namespace CoreLeaf.Console
{
    public interface IConsole
    {
        /// <summary>
        /// Gets and sets the left position of the cursor
        /// </summary>
        int CursorLeft { get; set; }

        /// <summary>
        /// Gets and sets the top position of the cursor
        /// </summary>
        int CursorTop { get; set; }

        /// <summary>
        /// Gets and sets the foreground color of the console
        /// </summary>
        ConsoleColor Foreground { get; set; }

        /// <summary>
        /// Gets and sets the background color of the console
        /// </summary>
        ConsoleColor Background { get; set; }

        /// <summary>
        /// Preserves the current position of the cursor. To restore the position, dispose the returned object
        /// </summary>
        /// <returns>The preserved position</returns>
        IDisposable PreserveCursorPosition();

        /// <summary>
        /// Preserves the current colors of the console. To restore, dispose the returned object
        /// </summary>
        /// <returns>The preserved colors</returns>
        IDisposable PreserveColor();

        /// <summary>
        /// Clears the current line
        /// </summary>
        void ClearLine();

        /// <summary>
        /// Clears the specified line
        /// </summary>
        /// <param name="lineNumber">The line to clear</param>
        void ClearLine(int lineNumber);

        /// <summary>
        /// Gets the cancel token that is set when the user presses the cancel key
        /// </summary>
        /// <returns></returns>
        CancellationToken GetCancelToken();

        /// <summary>
        /// Writes a message to the console using the specified color
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <param name="foreground">The foreground color to use</param>
        void Write(string message, ConsoleColor foreground = ConsoleColor.Gray);

        /// <summary>
        /// Writes a message to the console using the specified color followed by a newline
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <param name="foreground">The foreground color to use</param>
        void WriteLine(string message, ConsoleColor foreground = ConsoleColor.Gray);

        /// <summary>
        /// Reads the next line of characters from the input stream
        /// </summary>
        /// <returns>the next line of characters from the input stream</returns>
        string ReadLine();

        /// <summary>
        /// Reads content from the console, but hides the users typing
        /// </summary>
        /// <returns>The content provided by the user</returns>
        string ReadHiddenContent();
    }
}
