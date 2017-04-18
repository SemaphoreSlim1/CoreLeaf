using System.Threading;
using System.Threading.Tasks;

namespace CoreLeaf.NissanApi.Initial
{
    public interface IInitialAppClient
    {
        Task<string> GetEncryptionTokenAsync(CancellationToken cancelToken);
    }
}
