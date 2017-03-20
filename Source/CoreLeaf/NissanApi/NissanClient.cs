using CoreLeaf.Configuration;
using CoreLeaf.Net;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLeaf.NissanApi
{
    public abstract class NissanClient
    {
        protected readonly IRestClient _client;
        protected readonly IConfiguration _config;

        public NissanClient(IConfiguration config, IRestClient client)
        {
            _config = config;
            _client = client;

            var baseUri = _config[ConfigurationKeys.NissanBaseUri];
            _client.BaseUri = new Uri(baseUri);
        }

        protected async Task<TResponse> GetAsync<TResponse>(string route, CancellationToken cancelToken)
        {
            return await _client.GetAsync<TResponse>(route, cancelToken);
        }

        protected async Task<TResponse> PutAsync<TResponse>(string route, IEnumerable<KeyValuePair<string, string>> body, CancellationToken cancelToken)
        {
            return await _client.PutAsync<IEnumerable<KeyValuePair<string, string>>, TResponse>(route, body, cancelToken);
        }

        protected async Task<TResponse> PostAsync<TResponse>(string route, IEnumerable<KeyValuePair<string, string>> body, CancellationToken cancelToken)
        {
            return await _client.PostAsync<IEnumerable<KeyValuePair<string, string>>, TResponse>(route, body, cancelToken);
        }

        protected async Task<TResponse> DeleteAsync<TResponse>(string route, CancellationToken cancelToken)
        {
            return await _client.DeleteAsync<TResponse>(route, cancelToken);
        }
    }
}
