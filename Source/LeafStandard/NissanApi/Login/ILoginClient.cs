using ConsoleAbstractions.Autofac.Interception;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLeaf.NissanApi.Login
{
    public interface ILoginClient
    {
        [BeforeMessage("About to log in...")]
        [AfterMessage("Logged in")]
        Task<LoginResponse> LoginAsync(string encryptionToken, string userName, string password, CancellationToken cancelToken);
    }
}
