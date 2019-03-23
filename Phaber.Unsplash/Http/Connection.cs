using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Phaber.Unsplash.Helpers;

namespace Phaber.Unsplash.Http {
    public class Connection {
        private HttpClient _client;
        private Credentials _credentials;
        private IValidatableHttpResponse _responseHandler;

        public Connection(
            HttpClient client,
            Credentials credentials,
            IValidatableHttpResponse responseHandler
        ) {
            _client = client;
            _responseHandler = responseHandler;
            _credentials = credentials;
        }

        public Task<HttpResponseMessage> MakeRequest(
            Func<HttpClient, HttpRequestMessage, Task<HttpResponseMessage>> requestHandler
        ) {
            return requestHandler(
                _client, _credentials.Apply(new HttpRequestMessage())
            );
        }

        public async Task<Response<T>> MakeRequest<T>(
            Func<HttpClient, HttpRequestMessage, Task<HttpResponseMessage>> requestHandler,
            Func<HttpResponseMessage, Task<T>> deserializationHandler
        ) {
            var response = await requestHandler(
                _client, _credentials.Apply(new HttpRequestMessage())
            );

            _responseHandler.Handle(response);

            response.EnsureSuccessStatusCode();

            return new Response<T>(await deserializationHandler(response), response);
        }

        public async Task<Response<T>> MakeRequest<T>(
            Func<HttpClient, HttpRequestMessage, Task<HttpResponseMessage>> requestHandler,
            Func<HttpResponseBody, Task<T>> deserializationHandler
        ) {
            var response = await requestHandler(
                _client, _credentials.Apply(new HttpRequestMessage())
            );

            _responseHandler.Handle(response);

            response.EnsureSuccessStatusCode();

            return new Response<T>(
                await deserializationHandler(new HttpResponseBody(response.Content)),
                response
            );
        }

        public Task<HttpResponseMessage> MakeRequest(Uri endpoint, HttpMethod method) {
            return _client.SendAsync(new HttpRequestMessage(method, endpoint));
        }

        public Task<HttpResponseMessage> MakeStreamRequest(Uri endpoint, HttpMethod method) {
            return _client.SendAsync(
                new HttpRequestMessage(method, endpoint),
                HttpCompletionOption.ResponseHeadersRead
            );
        }

        public async Task<Response<T>> MakeRequest<T>(
            Uri endpoint,
            HttpMethod method,
            Func<HttpResponseBody, Task<T>> deserializationHandler
        ) {
            return await MakeRequest<T>(
                endpoint,
                method,
                new Dictionary<string, string>(),
                deserializationHandler
            );
        }

        public async Task<Response<T>> MakeRequest<T>(
            Uri endpoint,
            HttpMethod method,
            Dictionary<string, string> headers,
            Func<HttpResponseBody, Task<T>> deserializationHandler
        ) {

            var request = new HttpRequestBuilder(
                _credentials.Apply(new HttpRequestMessage())
            ).SetMethod(method).AddHeaders(headers).Done;

            var response = await _client.SendAsync(request);

            _responseHandler.Handle(response);

            response.EnsureSuccessStatusCode();

            return new Response<T>(
                await deserializationHandler(new HttpResponseBody(response.Content)),
                response
            );
        }

        public PagedResponse<T> MakePagedRequest<T>(
            Uri endpoint,
            int page,
            int perPage,
            HttpMethod method,
            Func<HttpResponseBody, Task<T>> deserializationHandler
        ) {
            var linkToPage = endpoint.AddQueries(
                new Dictionary<string, string>() {
                    { "page", $"{page}" },
                    { "per_page", $"{perPage}" }
                }
            );

            return new PagedResponse<T>(
                Page.Initial(linkToPage, page - 1, perPage),
                async endpointWithQuery => await MakeRequest(
                    endpointWithQuery,
                    method,
                    deserializationHandler
                )
            );
        }
    }
}