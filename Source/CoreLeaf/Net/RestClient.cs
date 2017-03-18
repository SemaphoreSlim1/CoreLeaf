using System;
using System.Net.Http;
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

        public RestClient(Func<HttpMessageHandler> handlerFactory,
            IHeaderProvider headerProvider,
            IContentEncoder contentEncoder,
            IResponseDeserializer responseDeserializer)
        {
            _handlerFactory = handlerFactory;
            HeaderProvider = headerProvider;
            ContentEncoder = contentEncoder;
            ResponseDeserializer = responseDeserializer;
            Timeout = TimeSpan.FromSeconds(30);
        }

        protected HttpClient SetupClient()
        {
            var client = new HttpClient(_handlerFactory())
            {
                Timeout = Timeout,
                BaseAddress = BaseUri
            };

            foreach (var kvp in HeaderProvider.GetHeaders())
            {
                client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value);
            }

            client.DefaultRequestHeaders.Accept.Clear();

            return client;
        }

        public async Task<TResponse> GetAsync<TResponse>(string route)
        {
            var client = SetupClient();
            var rawResponse = await client.GetAsync(route);
            var response = await ResponseDeserializer.Deserialize<TResponse>(rawResponse);
            return response;
        }
        public async Task<TResponse> PutAsync<TRequest, TResponse>(string route, TRequest body)
        {
            var client = SetupClient();

            var content = ContentEncoder.Encode(body);
            var rawResponse = await client.PutAsync(route, content);
            var response = await ResponseDeserializer.Deserialize<TResponse>(rawResponse);
            return response;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string route, TRequest body)
        {
            var client = SetupClient();

            var content = ContentEncoder.Encode(body);
            var rawResponse = await client.PostAsync(route, content);
            var response = await ResponseDeserializer.Deserialize<TResponse>(rawResponse);
            return response;

        }

        public async Task<TResponse> DeleteAsync<TResponse>(string route)
        {
            var client = SetupClient();
            var rawResponse = await client.DeleteAsync(route);
            var response = await ResponseDeserializer.Deserialize<TResponse>(rawResponse);
            return response;
        }
    }
}
