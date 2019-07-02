using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Phaber.Infrastructure.ErrorResolvers;
using Phaber.Infrastructure.Errors;
using Phaber.Infrastructure.Models;
using Phaber.Unsplash;
using Phaber.Unsplash.Errors;

namespace Phaber.Infrastructure.Http {
    public class HttpPipeline : IHttpPipeline {
        private readonly IErrorResolver<HttpResponseMessage> _errorResolver;
        private readonly ISerializer _serializer;

        public HttpPipeline(
            ISerializer serializer,
            IErrorResolver<HttpResponseMessage> errorResolver
        ) {
            _serializer = serializer;
            _errorResolver = errorResolver;
        }

        public HttpPipeline(
            IErrorResolver<HttpResponseMessage> errorResolver
        ) : this(
            new JsonSerializer(),
            errorResolver
        ) { }

        public HttpPipeline() : this(
            new ChainedErrorResolver<HttpResponseMessage>(
                new RateLimitErrorResolver(),
                new UnauthenticatedErrorResolver(),
                new ResourceNotFoundErrorResolver()
            )
        ) { }

        public HttpRequestMessage ApplyCredentials(
            HttpRequestMessage request,
            Credentials credentials
        ) {
            request.Headers.Add(
                "Authorization", $"Client-ID {credentials.ApplicationId}"
            );

            return request;
        }

        public IEnumerable<IError> ResolveErrors(HttpResponseMessage response) {
            return _errorResolver.Resolve(response);
        }

        public HttpRequestMessage ApplyBody(HttpRequestMessage request, object body) {
            request.Content = new StringContent(_serializer.Serialize(body));

            return request;
        }

        public async Task<FallibleBodyResponse<TB>> ExtractBody<TB>(
            HttpResponseMessage response
        ) where TB : class {
            var decoded = await response.Content.ReadAsStringAsync();

            try {
                return FallibleBodyResponse<TB>.OfSuccessful(
                    _serializer.Deserialize<TB>(decoded)
                );
            }
            catch (JsonSerializationException) {
                return FallibleBodyResponse<TB>.OfFailure(
                    new Error(
                        decoded,
                        "wrong format or structure of response body"
                    )
                );
            }
        }

        public async Task<string> ExtractBodyAsPlain(HttpResponseMessage response) {
            return await response.Content.ReadAsStringAsync();
        }
    }
}