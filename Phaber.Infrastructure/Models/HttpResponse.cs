using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Optional;
using Phaber.Unsplash;
using Phaber.Unsplash.Errors;
using Phaber.Unsplash.Helpers;

namespace Phaber.Infrastructure.Models {
    public class HttpResponse<TV> : FallibleBodyResponse<TV>, IHttpResponseMetadata where TV : class {
        public HttpStatusCode StatusCode { get; private set; }
        public IReadOnlyDictionary<string, string> Headers { get; private set; }

        private readonly Lazy<RateLimit> _rateLimit;
        public RateLimit RateLimit => _rateLimit.Value;

        protected HttpResponse(
            Option<TV> valuable,
            HttpResponseMessage httpResponse,
            IEnumerable<IError> errors
        ) : base(valuable, errors) {
            StatusCode = httpResponse.StatusCode;
            Headers = httpResponse
                .Headers
                .ToDictionary(h => h.Key, h => h.Value.Aggregate());

            _rateLimit = new Lazy<RateLimit>(
                () => new RateLimit(Headers)
            );
        }

        public static HttpResponse<TV> OfSuccessful(
            TV body,
            HttpResponseMessage response
        ) {
            return new HttpResponse<TV>(
                Option.Some(body),
                response,
                new List<IError>()
            );
        }

        public static HttpResponse<TV> OfFailure(
            TV body,
            HttpResponseMessage response,
            params IError[] errors
        ) {
            return new HttpResponse<TV>(
                Option.Some(body),
                response,
                errors.ToList()
            );
        }

        public static HttpResponse<TV> OfFailure(
            HttpResponseMessage response,
            params IError[] errors
        ) {
            return new HttpResponse<TV>(
                Option.None<TV>(),
                response,
                errors.ToList()
            );
        }

        public static HttpResponse<TV> OfFailure(
            TV body,
            HttpResponseMessage response,
            IEnumerable<IError> errors
        ) {
            return new HttpResponse<TV>(
                Option.Some(body),
                response,
                errors
            );
        }

        public static HttpResponse<TV> OfFailure(
            HttpResponseMessage response,
            IEnumerable<IError> errors
        ) {
            return new HttpResponse<TV>(
                Option.None<TV>(),
                response,
                errors
            );
        }
    }
}