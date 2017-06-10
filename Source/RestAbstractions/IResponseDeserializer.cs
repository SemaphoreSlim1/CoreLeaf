using System.Net.Http;
using System.Threading.Tasks;

namespace RestAbstractions
{
    public interface IResponseDeserializer
    {
        Task<TResponse> DeserializeAsync<TResponse>(HttpResponseMessage response);
    }
}
