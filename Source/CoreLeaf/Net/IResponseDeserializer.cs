using System.Net.Http;
using System.Threading.Tasks;

namespace CoreLeaf.Net
{
    public interface IResponseDeserializer
    {
        Task<TResponse> DeserializeAsync<TResponse>(HttpResponseMessage response);
    }
}
