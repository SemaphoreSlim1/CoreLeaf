using System;
using System.Threading;

namespace CoreLeaf.Console
{
    public interface IConsole
    {
        /// <summary>
        /// Gets the cancel token that is set when the user presses the cancel key
        /// </summary>
        /// <returns></returns>
        CancellationToken CancelToken { get; }

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

        #region Preserve cursor

        /// <summary>
        /// Preserves the current top and left position of the cursor. To restore the position, dispose the returned object
        /// </summary>
        /// <returns>The preserved position</returns>
        IDisposable PreserveCursor();

        /// <summary>
        /// Preserves only the top position of the cursor. To restore, dispose the returned object
        /// </summary>
        /// <returns>The preserved position</returns>
        IDisposable PreserveCursorTop();

        /// <summary>
        /// Preserves only the left position of the cursor. To restore, dispose the returned object
        /// </summary>
        /// <returns>The preserved position</returns>
        IDisposable PreserveCursorLeft();

        #endregion

        #region Preserve color

        /// <summary>
        /// Preserves the foreground and background colors of the console. To restore, dispose the returned object
        /// </summary>
        /// <returns>The preserved colors</returns>
        IDisposable PreserveColor();

        /// <summary>
        /// Preserves the foreground colors of the console. To restore, dispose the returned object.
        /// </summary>
        /// <returns>The preserved color</returns>
        IDisposable PreserveForeground();

        /// <summary>
        /// Preserves the background color of the console. To restore, dispose the returned object.
        /// </summary>
        /// <returns>The preserved color</returns>
        IDisposable PreserveBackground();

        #endregion

        #region Clear Line

        /// <summary>
        /// Clears the current line
        /// </summary>
        /// <param name="setLeftToZero">Sets the cursor left to zero after clearing the line, or leaves it alone if false</param>
        void ClearLine(bool setLeftToZero = true);

        /// <summary>
        /// Clears the specified line
        /// </summary>
        /// <param name="lineNumber">The line to clear</param>
        /// <param name="setLeftToZero">Sets the cursor left to zero after clearing the line, or leaves it alone if false</param>
        void ClearLine(int lineNumber, bool setLeftToZero = true);

        #endregion

        #region Write

        /// <summary>
        /// Writes a message to the console
        /// </summary>
        /// <param name="message">The message to write</param>
        void Write(string message);

        /// <summary>
        /// Writes a message to the console
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <param name="foreground">The foreground color to use</param>
        void Write(string message, ConsoleColor foreground);

        /// <summary>
        /// Writes a message to the console using the specified colors
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <param name="foreground">The foreground color to use</param>
        /// <param name="background">The background color to use</param>
        void Write(string message, ConsoleColor foreground, ConsoleColor background);

        #endregion

        #region WriteLine

        /// <summary>
        /// Writes a blank line to the console
        /// </summary>
        void WriteLine();

        /// <summary>
        /// Writes a message to the console followed by a newline
        /// </summary>
        /// <param name="message">The message to write</param>
        void WriteLine(string message);

        /// <summary>
        /// Writes a message to the console followed by a newline
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <param name="foreground">The foreground color to use</param>
        void WriteLine(string message, ConsoleColor foreground);

        /// <summary>
        /// Writes a message to the console using the specified color followed by a newline
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <param name="foreground">The foreground color to use</param>
        /// <param name="background">The background color to use</param>
        void WriteLine(string message, ConsoleColor foreground, ConsoleColor background);

        #endregion

        #region ReadLine

        /// <summary>
        /// Reads the next line of characters from the input stream
        /// </summary>
        /// <param name="mask">Whether or not to mask the user's typing</param>
        /// <returns>the next line of characters from the input stream</returns>
        string ReadLine(bool mask = false);

        /// <summary>
        /// Prompts the user for content
        /// </summary>        
        /// <param name="prompt">The prompt to present to the user</param>
        /// <param name="mask">Whether or not to mask the user's typing</param>
        /// <returns>the next line of characters from the input stream</returns>
        string ReadLine(string prompt, bool mask = false);

        /// <summary>
        /// Prompts the user for content, using the specified foreground color for the prompt
        /// </summary>
        /// <param name="prompt">The prompt to present the user</param>
        /// <param name="foreground">the foreground color to use</param>
        /// <param name="mask">Whether or not to mask the user's typing</param>
        /// <returns>the next line of characters from the input stream</returns>
        string ReadLine(string prompt, ConsoleColor foreground, bool mask = false);

        /// <summary>
        /// Prompts the user for content, using the specified colors for the prompt
        /// </summary>
        /// <param name="prompt">The prompt to present the user</param>
        /// <param name="foreground">the foreground color to use</param>
        /// <param name="background">the background color to use</param>
        /// <param name="mask">Whether or not to mask the user's typing</param>
        /// <returns>the next line of characters from the input stream</returns>
        string ReadLine(string prompt, ConsoleColor foreground, ConsoleColor background, bool mask = false);

        #endregion
    }
}