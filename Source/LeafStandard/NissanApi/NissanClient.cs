using LeafStandard.NissanApi;
using RestAbstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLeaf.NissanApi
{
    public abstract class NissanClient
    {
        protected readonly IRestClient _client;
        protected readonly string _apiKey;

        public NissanClient(IRestClient client, string apiKey)
        {            
            _client = client;
            _apiKey = apiKey;
        }

        private T ExtractResponse<T>(RestResponse<T> restResponse)
        {
            if (restResponse.RawResponse.IsSuccessStatusCode == false)
            {
                throw new Exception(restResponse.RawResponse.ReasonPhrase);
            }

            return restResponse.Data;
        }

        protected async Task<TResponse> GetAsync<TResponse>(string route, CancellationToken cancelToken)
        {
            var restResponse = await _client.GetAsync<TResponse>(route, cancelToken);

            return ExtractResponse(restResponse);
        }        

        protected async Task<TResponse> PutAsync<TResponse>(string route, BodyArgs body, CancellationToken cancelToken)
        {
            var restResponse = await _client.PutAsync<IEnumerable<KeyValuePair<string, string>>, TResponse>(route, body.Args, cancelToken);

            return ExtractResponse(restResponse);
        }

        protected async Task<TResponse> PostAsync<TResponse>(string route, BodyArgs body, CancellationToken cancelToken)
        {
            var restResponse = await _client.PostAsync<IEnumerable<KeyValuePair<string, string>>, TResponse>(route, body.Args, cancelToken);

            return ExtractResponse(restResponse);
        }

        protected async Task<TResponse> DeleteAsync<TResponse>(string route, CancellationToken cancelToken)
        {
            var restResponse = await _client.DeleteAsync<TResponse>(route, cancelToken);

            return ExtractResponse(restResponse);
        }
    }
}
