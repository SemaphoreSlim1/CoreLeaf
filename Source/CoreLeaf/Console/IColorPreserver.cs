using System;

namespace CoreLeaf.Console
{
    /// <summary>
    /// Preserves the console colors, and restores them upon disposing
    /// </summary>
    public interface IColorPreserver : IDisposable
    {
        /// <summary>
        /// Gets the preserved background color
        /// </summary>
        ConsoleColor Background { get; }

        /// <summary>
        /// Gets the preserved foreground color
        /// </summary>
        ConsoleColor Foreground { get; }
    }
}
