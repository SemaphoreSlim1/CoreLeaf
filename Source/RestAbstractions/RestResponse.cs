using System.Net.Http;

namespace RestAbstractions
{
    public class RestResponse<T>
    {
        public T Data { get; }
        public string Content { get; }
        public HttpResponseMessage RawResponse { get; }

        public RestResponse(T data, string content, HttpResponseMessage rawResponse)
        {
            Data = data;
            Content = content;
            RawResponse = rawResponse;
        }
    }
}
