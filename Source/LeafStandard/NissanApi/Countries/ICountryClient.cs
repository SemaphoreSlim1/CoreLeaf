using ConsoleAbstractions.Autofac.Interception;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLeaf.NissanApi.Countries
{
    public interface ICountryClient
    {
        [BeforeMessage("About to get settings...")]
        [AfterMessage("Retrieved settings")]
        Task<IDictionary<string, bool>> GetSettingsAsync(CancellationToken cancelToken);
    }
}
