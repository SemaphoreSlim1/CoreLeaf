using System;
using System.Threading.Tasks;

namespace CoreLeaf.Net
{
    public interface IRestClient
    {
        /// <summary>
        /// Gets and sets The base URI 
        /// </summary>
        Uri BaseUri { get; set; }

        /// <summary>
        /// Get and sets the timeout
        /// </summary>
        TimeSpan Timeout { get; set; }

        /// <summary>
        /// Gets and sets the instance which will provide headers
        /// </summary>
        IHeaderProvider HeaderProvider { get; set; }

        /// <summary>
        /// Gets and sets the deserializer for the response
        /// </summary>
        IResponseDeserializer ResponseDeserializer { get; set; }

        /// <summary>
        /// Gets and sets the encoder for the body of requests
        /// </summary>
        IContentEncoder ContentEncoder { get; set; }

        /// <summary>
        /// Issues a HTTP GET to the route, uses <see cref="ResponseDeserializer"/> to deserialize to <typeparamref name="TResponse"/>
        /// </summary>
        /// <typeparam name="TResponse">The type of the response</typeparam>
        /// <param name="route">the route to issue the HTTP Get agains</param>
        /// <returns></returns>
        Task<TResponse> GetAsync<TResponse>(string route);

        Task<TResponse> PutAsync<TRequest, TResponse>(string route, TRequest body);

        Task<TResponse> PostAsync<TRequest, TResponse>(string route, TRequest body);

        Task<TResponse> DeleteAsync<TResponse>(string route);
    }
}
