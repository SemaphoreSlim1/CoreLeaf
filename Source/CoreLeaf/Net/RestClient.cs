using CoreLeaf.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLeaf.Net
{
    public class RestClient : IRestClient
    {
        public Uri BaseUri { get; set; }

        public IResponseDeserializer ResponseDeserializer { get; set; }

        public IContentEncoder ContentEncoder { get; set; }

        public TimeSpan Timeout { get; set; }

        public IHeaderProvider HeaderProvider { get; set; }

        private Func<HttpMessageHandler> _handlerFactory;

        public RestClient(IConfiguration config,
            Func<HttpMessageHandler> handlerFactory,
            IHeaderProvider headerProvider,
            IContentEncoder contentEncoder,
            IResponseDeserializer responseDeserializer)
        {
            _handlerFactory = handlerFactory;
            HeaderProvider = headerProvider;
            ContentEncoder = contentEncoder;
            ResponseDeserializer = responseDeserializer;

            var timeoutString = config[ConfigurationKeys.DefaultHttpTimeout];
            Timeout = TimeSpan.Parse(timeoutString);
        }

        protected HttpClient SetupClient()
        {
            var client = new HttpClient(_handlerFactory())
            {
                Timeout = Timeout,
                BaseAddress = BaseUri
            };

            var headers = HeaderProvider.GetHeaders();
            if (headers != null)
            {
                foreach (var kvp in HeaderProvider.GetHeaders())
                {
                    client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
                }
            }

            client.DefaultRequestHeaders.Accept.Clear();

            return client;
        }

        public async Task<TResponse> GetAsync<TResponse>(string route, CancellationToken cancelToken)
        {
            var client = SetupClient();
            var rawResponse = await client.GetAsync(route, cancelToken);
            var response = await ResponseDeserializer.DeserializeAsync<TResponse>(rawResponse);
            return response;
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string route, TRequest body, CancellationToken cancelToken)
        {
            var client = SetupClient();

            var content = ContentEncoder.Encode(body);
            var rawResponse = await client.PutAsync(route, content, cancelToken);
            var response = await ResponseDeserializer.DeserializeAsync<TResponse>(rawResponse);
            return response;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string route, TRequest body, CancellationToken cancelToken)
        {
            var client = SetupClient();

            var content = ContentEncoder.Encode(body);
            var rawResponse = await client.PostAsync(route, content, cancelToken);
            var response = await ResponseDeserializer.DeserializeAsync<TResponse>(rawResponse);
            return response;

        }

        public async Task<TResponse> DeleteAsync<TResponse>(string route, CancellationToken cancelToken)
        {
            var client = SetupClient();
            var rawResponse = await client.DeleteAsync(route, cancelToken);
            var response = await ResponseDeserializer.DeserializeAsync<TResponse>(rawResponse);
            return response;
        }
    }
}
