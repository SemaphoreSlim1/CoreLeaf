using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RestAbstractions
{
    public class RestClient : IRestClient
    {
        public Uri BaseUri { get; }

        public IResponseDeserializer ResponseDeserializer { get; }

        public IContentEncoder ContentEncoder { get; }

        public TimeSpan Timeout { get; }

        private readonly HttpClient _client;

        public RestClient(Uri baseUri,  TimeSpan timeout,
            Func<HttpMessageHandler> handlerFactory,
            IContentEncoder contentEncoder,
            IResponseDeserializer responseDeserializer)
        {            
            ContentEncoder = contentEncoder;
            ResponseDeserializer = responseDeserializer;
            Timeout = timeout;
            BaseUri = baseUri;

            _client = new HttpClient(handlerFactory())
            {
                Timeout = Timeout,
                BaseAddress = BaseUri
            };

            _client.DefaultRequestHeaders.Accept.Clear();
        }

        public void AddHeader(string key, string value)
        {
            _client.DefaultRequestHeaders.Add(key, value);            
        }
        

        public async Task<RestResponse<T>> GetAsync<T>(string route, CancellationToken cancelToken)
        {
            var rawResponse = await _client.GetAsync(route, cancelToken);
            var response = await ResponseDeserializer.DeserializeAsync<T>(rawResponse);
                       
            return response;
        }

        public async Task<RestResponse<TResponse>> PutAsync<TRequest, TResponse>(string route, TRequest body, CancellationToken cancelToken)
        {
            var requestContent = ContentEncoder.Encode(body);
            var rawResponse = await _client.PutAsync(route, requestContent, cancelToken);
            var response = await ResponseDeserializer.DeserializeAsync<TResponse>(rawResponse);

            return response;
        }

        public async Task<RestResponse<TResponse>> PostAsync<TRequest, TResponse>(string route, TRequest body, CancellationToken cancelToken)
        {
            var requestContent = ContentEncoder.Encode(body);
            var rawResponse = await _client.PostAsync(route, requestContent, cancelToken);
            var response = await ResponseDeserializer.DeserializeAsync<TResponse>(rawResponse);

            return response;
        }

        public async Task<RestResponse<T>> DeleteAsync<T>(string route, CancellationToken cancelToken)
        {
            var rawResponse = await _client.DeleteAsync(route, cancelToken);
            var data = await ResponseDeserializer.DeserializeAsync<T>(rawResponse);
            var response = await ResponseDeserializer.DeserializeAsync<T>(rawResponse);

            return response;
        }
    }
}
