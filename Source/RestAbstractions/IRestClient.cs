using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestAbstractions
{
    public interface IRestClient
    {
        /// <summary>
        /// Gets and sets The base URI 
        /// </summary>
        Uri BaseUri { get; }

        /// <summary>
        /// Get and sets the timeout
        /// </summary>
        TimeSpan Timeout { get; }

        void AddHeader(string key, string value);

        /// <summary>
        /// Gets and sets the deserializer for the response
        /// </summary>
        IResponseDeserializer ResponseDeserializer { get; }

        /// <summary>
        /// Gets and sets the encoder for the body of requests
        /// </summary>
        IContentEncoder ContentEncoder { get; }

        /// <summary>
        /// Issues a HTTP GET to the route
        /// </summary>
        /// <typeparam name="T">The type of the response</typeparam>
        /// <param name="route">the route to issue the HTTP Get against</param>
        /// <param name="cancelToken">A cancellation token that can be used by other threads to signal cancellation</param>
        /// <returns>the response from the http endpoint</returns>
        Task<RestResponse<T>> GetAsync<T>(string route, CancellationToken cancelToken);

        /// <summary>
        /// Issues a HTTP PUT to the route
        /// </summary>
        /// <typeparam name="TRequest">the type of the request</typeparam>
        /// <typeparam name="TResponse">the type of the repsonse</typeparam>
        /// <param name="route">the route to issue the HTTP PUT against</param>
        /// <param name="body">The body of the request</param>
        /// <param name="cancelToken">A cancellation token that can be used by other threads to signal cancellation</param>
        /// <returns>the response from the http endpoint</returns>
        Task<RestResponse<TResponse>> PutAsync<TRequest, TResponse>(string route, TRequest body, CancellationToken cancelToken);

        /// <summary>
        /// Issues a HTTP POST to the route
        /// </summary>
        /// <typeparam name="TRequest">the type of the request</typeparam>
        /// <typeparam name="TResponse">the type of the response</typeparam>
        /// <param name="route">The route to issue the HTTP POST against</param>
        /// <param name="body">the body of the request</param>
        /// <param name="cancelToken">A cancellation token that can be used by other threads to signal cancellation</param>
        /// <returns>the response from the http endpoint</returns>
        Task<RestResponse<TResponse>> PostAsync<TRequest, TResponse>(string route, TRequest body, CancellationToken cancelToken);

        /// <summary>
        /// Issues a HTTP DELETE to the route
        /// </summary>
        /// <typeparam name="T">the type of the response</typeparam>
        /// <param name="route">the route to issue the HTTP DELETE against</param>
        /// <param name="cancelToken">A cancellation token that can be used by other threads to signal cancellation</param>
        /// <returns>the response from the http endpoint</returns>
        Task<RestResponse<T>> DeleteAsync<T>(string route, CancellationToken cancelToken);
    }
}
