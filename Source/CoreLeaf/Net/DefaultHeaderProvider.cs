using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLeaf.Net
{
    public class DefaultHeaderProvider : IHeaderProvider
    {
        private Dictionary<string, IEnumerable<string>> _defaultHeaders;

        public DefaultHeaderProvider()
        {
            _defaultHeaders = new Dictionary<string, IEnumerable<string>>();
        }
        public IDictionary<string, IEnumerable<string>> GetHeaders()
        {
            return _defaultHeaders;
        }
    }
}
