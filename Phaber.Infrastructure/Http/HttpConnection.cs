using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Optional.Unsafe;
using Phaber.Infrastructure.Models;
using Phaber.Unsplash;
using Phaber.Unsplash.Helpers;
using Phaber.Unsplash.Models;

namespace Phaber.Infrastructure.Http {
    public class HttpConnection : IConnection {
        private readonly HttpClient _client;
        private readonly Credentials _credentials;
        private readonly IHttpPipeline _httpPipeline;

        public HttpConnection(
            HttpClient client,
            IHttpPipeline httpPipeline,
            Credentials credentials
        ) {
            _client = client;
            _httpPipeline = httpPipeline;
            _credentials = credentials;
        }

        public HttpConnection(HttpClient client, Credentials credentials) : this(
            client,
            new HttpPipeline(),
            credentials
        ) { }

        public HttpConnection(Credentials credentials) : this(
            new HttpClient(),
            credentials
        ) { }

        public Task<IFallibleBodyResponse<T>> MakeRequest<T>(
            Uri endpoint,
            HttpMethod method
        ) where T : class {
            return MakeRequest<T>(
                endpoint,
                new Dictionary<string, string>(),
                method
            );
        }

        public async Task<IFallibleBodyResponse<T>> MakeRequest<T>(
            Uri endpoint,
            Dictionary<string, string> headers,
            HttpMethod method
        ) where T : class {
            var request = new HttpRequestBuilder(new HttpRequestMessage())
                .SetMethod(method)
                .AddHeaders(headers)
                .SetEndpoint(endpoint)
                .Done;

            var authenticatedRequest = _httpPipeline.ApplyCredentials(request, _credentials);

            return await SendRequest<T>(authenticatedRequest);
        }

        public Task<IFallibleBodyResponse<T>> MakeRequest<T, TB>(
            Uri endpoint,
            TB body,
            HttpMethod method
        ) where T : class {
            return MakeRequest<T, TB>(
                endpoint,
                new Dictionary<string, string>(),
                body,
                method
            );
        }

        public async Task<IFallibleBodyResponse<T>> MakeRequest<T, TB>(
            Uri endpoint,
            Dictionary<string, string> headers,
            TB body,
            HttpMethod method
        ) where T : class {
            var request = new HttpRequestBuilder(new HttpRequestMessage())
                .SetMethod(method)
                .AddHeaders(headers)
                .SetEndpoint(endpoint)
                .Done;

            var authenticatedRequest = _httpPipeline.ApplyCredentials(request, _credentials);
            var requestWithBody = _httpPipeline.ApplyBody(authenticatedRequest, body);

            return await SendRequest<T>(requestWithBody);
        }

        private async Task<IFallibleBodyResponse<T>> SendRequest<T>(
            HttpRequestMessage request
        ) where T : class {
            var response = await _client.SendAsync(request);
            var errors = _httpPipeline.ResolveErrors(response);

            if (errors.Any()) {
                return HttpResponse<T>.OfFailure(response, errors);
            }

            var deserialized = await _httpPipeline.ExtractBody<T>(response);

            if (!deserialized.IsSuccess) {
                return HttpResponse<T>.OfFailure(response, deserialized.Errors);
            }

            return HttpResponse<T>.OfSuccessful(
                deserialized.Retrieve().ValueOrFailure()
            );
        }

        public Task<IFallibleBodyResponse<T>> MakeRequest<T>(
            Uri endpoint,
            HttpMethod method,
            Func<string, T> deserialize
        ) where T : class {
            return MakeRequest(
                endpoint,
                new Dictionary<string, string>(),
                method,
                deserialize
            );
        }

        public async Task<IFallibleBodyResponse<T>> MakeRequest<T>(
            Uri endpoint,
            Dictionary<string, string> headers,
            HttpMethod method,
            Func<string, T> deserialize
        ) where T : class {
            var request = new HttpRequestBuilder(new HttpRequestMessage())
                .SetMethod(method)
                .AddHeaders(headers)
                .SetEndpoint(endpoint)
                .Done;

            var authenticatedRequest = _httpPipeline.ApplyCredentials(request, _credentials);

            return await SendRequest(authenticatedRequest, deserialize);
        }

        public Task<IFallibleBodyResponse<T>> MakeRequest<T, TB>(
            Uri endpoint,
            TB body,
            HttpMethod method,
            Func<string, T> deserialize
        ) where T : class {
            return MakeRequest(
                endpoint,
                new Dictionary<string, string>(),
                body,
                method,
                deserialize
            );
        }

        public async Task<IFallibleBodyResponse<T>> MakeRequest<T, TB>(
            Uri endpoint,
            Dictionary<string, string> headers,
            TB body,
            HttpMethod method,
            Func<string, T> deserialize
        ) where T : class {
            var request = new HttpRequestBuilder(new HttpRequestMessage())
                .SetMethod(method)
                .AddHeaders(headers)
                .SetEndpoint(endpoint)
                .Done;

            var authenticatedRequest = _httpPipeline.ApplyCredentials(request, _credentials);
            var requestWithBody = _httpPipeline.ApplyBody(authenticatedRequest, body);

            return await SendRequest(requestWithBody, deserialize);
        }

        private async Task<IFallibleBodyResponse<T>> SendRequest<T>(
            HttpRequestMessage request,
            Func<string, T> deserialize
        ) where T : class {
            var response = await _client.SendAsync(request);
            var errors = _httpPipeline.ResolveErrors(response);

            if (errors.Any()) {
                return HttpResponse<T>.OfFailure(response, errors);
            }

            var decoded = await _httpPipeline.ExtractBodyAsPlain(response);
            var deserialized = deserialize(decoded);

            return HttpResponse<T>.OfSuccessful(deserialized);
        }

        public Task<IFallibleResponse> MakePlainRequest(
            Uri endpoint,
            HttpMethod method
        ) {
            return MakePlainRequest(
                endpoint,
                new Dictionary<string, string>(),
                method
            );
        }

        public async Task<IFallibleResponse> MakePlainRequest(
            Uri endpoint,
            Dictionary<string, string> headers,
            HttpMethod method
        ) {
            var request = new HttpRequestBuilder(new HttpRequestMessage())
                .SetMethod(method)
                .AddHeaders(headers)
                .SetEndpoint(endpoint)
                .Done;

            var authenticatedRequest = _httpPipeline.ApplyCredentials(request, _credentials);

            return await SendPlainRequest(authenticatedRequest);
        }

        public Task<IFallibleResponse> MakePlainRequest<TB>(
            Uri endpoint,
            TB body,
            HttpMethod method
        ) {
            return MakePlainRequest(
                endpoint,
                new Dictionary<string, string>(),
                body,
                method
            );
        }

        public async Task<IFallibleResponse> MakePlainRequest<TB>(
            Uri endpoint,
            Dictionary<string, string> headers,
            TB body,
            HttpMethod method
        ) {
            var request = new HttpRequestBuilder(new HttpRequestMessage())
                .SetMethod(method)
                .AddHeaders(headers)
                .SetEndpoint(endpoint)
                .Done;

            var authenticatedRequest = _httpPipeline.ApplyCredentials(request, _credentials);
            var requestWithBody = _httpPipeline.ApplyBody(authenticatedRequest, body);

            return await SendPlainRequest(requestWithBody);
        }

        private async Task<IFallibleResponse> SendPlainRequest(
            HttpRequestMessage request
        ) {
            var response = await _client.SendAsync(request);
            var errors = _httpPipeline.ResolveErrors(response);

            if (errors.Any()) {
                return HttpEmptyResponse.OfFailure(response, errors);
            }

            return HttpEmptyResponse.OfSuccessful(response);
        }

        public Task<IFallibleBodyResponse<HttpResponseMessage>> MakeStreamRequest(
            Uri endpoint,
            HttpMethod method
        ) {
            return MakeStreamRequest(
                endpoint,
                new Dictionary<string, string>(),
                method
            );
        }

        public async Task<IFallibleBodyResponse<HttpResponseMessage>> MakeStreamRequest(
            Uri endpoint,
            Dictionary<string, string> headers,
            HttpMethod method
        ) {
            var request = new HttpRequestBuilder(new HttpRequestMessage())
                .SetMethod(method)
                .AddHeaders(headers)
                .SetEndpoint(endpoint)
                .Done;

            var authenticatedRequest = _httpPipeline.ApplyCredentials(request, _credentials);

            return await SendStreamRequest(authenticatedRequest);
        }

        public Task<IFallibleBodyResponse<HttpResponseMessage>> MakeStreamRequest<TB>(
            Uri endpoint,
            TB body,
            HttpMethod method
        ) {
            return MakeStreamRequest(
                endpoint,
                new Dictionary<string, string>(),
                body,
                method
            );
        }

        public async Task<IFallibleBodyResponse<HttpResponseMessage>> MakeStreamRequest<TB>(
            Uri endpoint,
            Dictionary<string, string> headers,
            TB body,
            HttpMethod method
        ) {
            var request = new HttpRequestBuilder(new HttpRequestMessage())
                .SetMethod(method)
                .AddHeaders(headers)
                .SetEndpoint(endpoint)
                .Done;

            var authenticatedRequest = _httpPipeline.ApplyCredentials(request, _credentials);
            var requestWithBody = _httpPipeline.ApplyBody(authenticatedRequest, body);

            return await SendStreamRequest(requestWithBody);
        }

        private async Task<IFallibleBodyResponse<HttpResponseMessage>> SendStreamRequest(
            HttpRequestMessage request
        ) {
            var response = await _client.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead
            );
            var errors = _httpPipeline.ResolveErrors(response);

            if (errors.Any()) {
                return HttpResponse<HttpResponseMessage>.OfFailure(response, errors);
            }

            return HttpResponse<HttpResponseMessage>.OfSuccessful(response);
        }

        public IPageableResponse<TV> MakePagedRequest<TV>(
            Uri endpoint,
            int page,
            int perPage,
            HttpMethod method
        ) where TV : class {
            var linkToPage = endpoint.AddQueries(
                new Dictionary<string, string> {
                    {"page", $"{page}"},
                    {"per_page", $"{perPage}"}
                }
            );

            return new PageableHttpResponse<TV>(
                Pageable.From(
                    new InitialPage(linkToPage, page, perPage)),
                async endpointToPage => await MakeRequest<TV>(
                    endpointToPage,
                    method
                ) as HttpResponse<TV>
            );
        }
    }
}