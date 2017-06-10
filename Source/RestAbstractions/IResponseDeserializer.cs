using System.Net.Http;
using System.Threading.Tasks;

namespace RestAbstractions
{
    public interface IResponseDeserializer
    {
        Task<RestResponse<T>> DeserializeAsync<T>(HttpResponseMessage response);
    }
}
