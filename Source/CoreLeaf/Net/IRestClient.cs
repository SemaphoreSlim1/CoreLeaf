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
        /// Issues a HTTP GET to the route
        /// </summary>
        /// <typeparam name="TResponse">The type of the response</typeparam>
        /// <param name="route">the route to issue the HTTP Get against</param>
        /// <returns>the response from the http endpoint</returns>
        Task<TResponse> GetAsync<TResponse>(string route);

        /// <summary>
        /// Issues a HTTP PUT to the route
        /// </summary>
        /// <typeparam name="TRequest">the type of the request</typeparam>
        /// <typeparam name="TResponse">the type of the repsonse</typeparam>
        /// <param name="route">the route to issue the HTTP PUT against</param>
        /// <param name="body">The body of the request</param>
        /// <returns>the response from the http endpoint</returns>
        Task<TResponse> PutAsync<TRequest, TResponse>(string route, TRequest body);

        /// <summary>
        /// Issues a HTTP POST to the route
        /// </summary>
        /// <typeparam name="TRequest">the type of the request</typeparam>
        /// <typeparam name="TResponse">the type of the response</typeparam>
        /// <param name="route">The route to issue the HTTP POST against</param>
        /// <param name="body">the body of the request</param>
        /// <returns>the response from the http endpoint</returns>
        Task<TResponse> PostAsync<TRequest, TResponse>(string route, TRequest body);

        /// <summary>
        /// Issues a HTTP DELETE to the route
        /// </summary>
        /// <typeparam name="TResponse">the type of the response</typeparam>
        /// <param name="route">the route to issue the HTTP DELETE against</param>
        /// <returns>the response from the http endpoint</returns>
        Task<TResponse> DeleteAsync<TResponse>(string route);
    }
}
