using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Phaber.Unsplash;
using Phaber.Unsplash.Errors;
using Phaber.Unsplash.Helpers;

namespace Phaber.Infrastructure.Models {
    public class HttpEmptyResponse : FallibleResponse, IHttpResponseMetadata {
        public HttpStatusCode StatusCode { get; private set; }
        public IReadOnlyDictionary<string, string> Headers { get; private set; }

        private readonly Lazy<RateLimit> _rateLimit;
        public RateLimit RateLimit => _rateLimit.Value;

        protected HttpEmptyResponse(
            HttpResponseMessage httpResponse,
            IEnumerable<IError> errors
        ) : base(errors) {
            StatusCode = httpResponse.StatusCode;
            Headers = httpResponse
                .Headers
                .ToDictionary(h => h.Key, h => h.Value.Aggregate());

            _rateLimit = new Lazy<RateLimit>(
                () => new RateLimit(Headers)
            );
        }

        public static HttpEmptyResponse OfSuccessful(HttpResponseMessage response) {
            return new HttpEmptyResponse(response, new List<IError>());
        }

        public static HttpEmptyResponse OfFailure(
            HttpResponseMessage response,
            params IError[] errors
        ) {
            return new HttpEmptyResponse(response, errors.ToList());
        }

        public static HttpEmptyResponse OfFailure(
            HttpResponseMessage response,
            IEnumerable<IError> errors
        ) {
            return new HttpEmptyResponse(response, errors);
        }
    }
}