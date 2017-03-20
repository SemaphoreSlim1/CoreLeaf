using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLeaf.NissanApi.Countries
{
    public interface ICountryClient
    {
        Task<IDictionary<string, bool>> GetSettingsAsync(string apiKey, CancellationToken cancelToken);
    }
}
