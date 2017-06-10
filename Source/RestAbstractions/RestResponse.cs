using System.Net.Http;

namespace RestAbstractions
{
    public class RestResponse<T>
    {
        public T Data { get; }
        public HttpResponseMessage RawResponse { get; }

        public RestResponse(T data, HttpResponseMessage rawResponse)
        {
            Data = data;
            RawResponse = rawResponse;
        }
    }
}
