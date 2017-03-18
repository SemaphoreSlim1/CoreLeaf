using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLeaf.Console
{
    public interface ICursorPreserver : IDisposable
    {
        /// <summary>
        /// Gets the preserved top value
        /// </summary>
        int Top { get; }

        /// <summary>
        /// Gets the preserved left value
        /// </summary>
        int Left { get; }
    }
}
