using System.Collections.Generic;

namespace CoreLeaf.Net
{
    public interface IHeaderProvider
    {
        IDictionary<string, IEnumerable<string>> GetHeaders();
    }
}
