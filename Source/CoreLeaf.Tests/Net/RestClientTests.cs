using CoreLeaf.Net;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CoreLeaf.Tests.Net
{
    public class RestClientTests
    {
        /// <summary>
        /// Contains the expected values of the actual execution
        /// </summary>
        private class Expectation
        {
            public HttpMethod HttpMethod { get; set; }
            public Uri Uri { get; set; }
            public int Response { get; set; }
        }       

        private Func<HttpMessageHandler> GenerateHandlerFactory(int expectedResponse, Action<HttpRequestMessage> callbackAction)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns((HttpRequestMessage msg, CancellationToken token) =>
                {
                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent(expectedResponse.ToString());

                    return Task.FromResult(response);
                })
                .Callback<HttpRequestMessage, CancellationToken>((r, c) => 
                {
                    callbackAction(r);
                });


            return () => handlerMock.Object;

        }

        private IResponseDeserializer CreateDeserializerStub()
        {
            var mock = new Mock<IResponseDeserializer>();
            mock.Setup(x => x.DeserializeAsync<int>(It.IsAny<HttpResponseMessage>()))
                .Returns(async (HttpResponseMessage msg) => {
                    var content = await msg.Content.ReadAsStringAsync();
                    return Convert.ToInt32(content);
                });

            return mock.Object;
        }

        private IContentEncoder CreateContentEncoderStub()
        {
            var mock = new Mock<IContentEncoder>();
            mock.Setup(x => x.Encode<int>(It.IsAny<int>()))
                .Returns((int value) => new StringContent(value.ToString()));

            return mock.Object;
        }

        private RestClient CreateRestClient(Func<HttpMessageHandler> handlerFactory)
        {
            var headerProvider = Mock.Of<IHeaderProvider>();
            var contentEncoder = CreateContentEncoderStub();
            var responseDeserializer = CreateDeserializerStub();

            var client = new RestClient(handlerFactory, headerProvider, contentEncoder, responseDeserializer);
            client.BaseUri = new Uri("https://unittest.com/");

            return client;
        }

        [Theory]
        [InlineData(1)]
        public async Task RestClient_Get_SuccessfulResponse(int expectedResponse)
        {
            //arrange
            var expectation = new Expectation()
            {
                HttpMethod = HttpMethod.Get,
                Response = expectedResponse,
                Uri = new Uri("https://unittest.com/api/UnitTest")
            };

            HttpRequestMessage request = null;
            var callbackFn = new Action<HttpRequestMessage>(r => { request = r; });
            var handlerFactory = GenerateHandlerFactory(expectedResponse, callbackFn);

            var client = CreateRestClient(handlerFactory);       

            //act
            var actualResponse = await client.GetAsync<int>("api/UnitTest");

            AssertExecution(request, actualResponse, expectation);            
        }
        

        [Theory]
        [InlineData(2,1)]
        public async Task RestClient_Put_SuccessfulResponse(int requestValue, int expectedResponse)
        {
            //arrange
            var expectation = new Expectation()
            {
                HttpMethod = HttpMethod.Put,
                Response = expectedResponse,
                Uri = new Uri("https://unittest.com/api/UnitTest")
            };

            HttpRequestMessage request = null;
            var callbackFn = new Action<HttpRequestMessage>(r => { request = r; });
            var handlerFactory = GenerateHandlerFactory(expectedResponse, callbackFn);

            var client = CreateRestClient(handlerFactory);

            //act
            var response = await client.PutAsync<int,int>("api/UnitTest",requestValue);

            AssertExecution(request, response, expectation);
        }

        [Theory]
        [InlineData(2, 1)]
        public async Task RestClient_Post_SuccessfulResponse(int requestValue, int expectedResponse)
        {
            //arrange
            var expectation = new Expectation()
            {
                HttpMethod = HttpMethod.Post,
                Response = expectedResponse,
                Uri = new Uri("https://unittest.com/api/UnitTest")
            };

            HttpRequestMessage request = null;
            var callbackFn = new Action<HttpRequestMessage>(r => { request = r; });
            var handlerFactory = GenerateHandlerFactory(expectedResponse, callbackFn);

            var client = CreateRestClient(handlerFactory);

            //act
            var response = await client.PostAsync<int, int>("api/UnitTest", requestValue);

            AssertExecution(request, response, expectation);
        }

        [Theory]
        [InlineData(1)]
        public async Task RestClient_Delete_SuccessfulResponse(int expectedResponse)
        {
            //arrange
            var expectation = new Expectation()
            {
                HttpMethod = HttpMethod.Delete,
                Response = expectedResponse,
                Uri = new Uri("https://unittest.com/api/UnitTest")
            };

            HttpRequestMessage request = null;
            var callbackFn = new Action<HttpRequestMessage>(r => { request = r; });
            var handlerFactory = GenerateHandlerFactory(expectedResponse, callbackFn);

            var client = CreateRestClient(handlerFactory);

            //act
            var actualResponse = await client.DeleteAsync<int>("api/UnitTest");

            AssertExecution(request, actualResponse, expectation);
        }

        private void AssertExecution(HttpRequestMessage request, int actualResponse, Expectation expectedValues)
        {
            Assert.Equal(expectedValues.HttpMethod, request.Method);
            Assert.Equal(expectedValues.Uri, request.RequestUri);
            Assert.Equal(expectedValues.Response, actualResponse);
        }
    }
}
