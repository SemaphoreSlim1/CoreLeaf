using ConsoleAbstractions.Autofac.Interception;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLeaf.NissanApi.Initial
{
    public interface IInitialAppClient
    {
        [BeforeMessage("About to get the encryption token...")]
        [AfterMessage("Retrieved encryption token")]
        Task<string> GetEncryptionTokenAsync(CancellationToken cancelToken);
    }
}
