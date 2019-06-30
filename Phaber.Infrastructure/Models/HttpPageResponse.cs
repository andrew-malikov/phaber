using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Optional;
using Phaber.Infrastructure.Errors;
using Phaber.Unsplash;
using Phaber.Unsplash.Errors;
using Phaber.Unsplash.Helpers;
using Phaber.Unsplash.Models;

namespace Phaber.Infrastructure.Models {
    public class HttpPageResponse<TV> : FalliblePageResponse<TV>, IHttpResponseMetadata where TV : class {
        public HttpStatusCode StatusCode { get; private set; }
        public IReadOnlyDictionary<string, string> Headers { get; private set; }

        private readonly Lazy<RateLimit> _rateLimit;
        public RateLimit RateLimit => _rateLimit.Value;

        protected HttpPageResponse(
            TV valuable,
            IPageable pageable,
            HttpResponseMessage httpResponse,
            IEnumerable<IError> errors
        ) : this(
            valuable != null ? Option.Some(valuable) : Option.None<TV>(),
            pageable != null ? Option.Some(pageable) : Option.None<IPageable>(),
            httpResponse,
            errors
        ) { }

        protected HttpPageResponse(
            Option<TV> valuable,
            Option<IPageable> pageable,
            HttpResponseMessage httpResponse,
            IEnumerable<IError> errors
        ) : base(valuable, pageable, errors) {
            StatusCode = httpResponse.StatusCode;
            Headers = httpResponse
                .Headers
                .ToDictionary(h => h.Key, h => h.Value.Aggregate());

            _rateLimit = new Lazy<RateLimit>(
                () => new RateLimit(Headers)
            );
        }

        protected HttpPageResponse(
            Option<TV> valuable,
            Option<IPageable> pageable,
            HttpStatusCode statusCode,
            IReadOnlyDictionary<string, string> headers,
            IEnumerable<IError> errors
        ) : base(valuable, pageable, errors) {
            StatusCode = statusCode;
            Headers = headers;

            _rateLimit = new Lazy<RateLimit>(
                () => new RateLimit(Headers)
            );
        }

        public static HttpPageResponse<TV> OfSuccessful(
            TV body,
            IPageable pageable,
            HttpResponseMessage response
        ) {
            return new HttpPageResponse<TV>(
                body,
                pageable,
                response,
                new List<IError>()
            );
        }

        public static HttpPageResponse<TV> OfSuccessful(
            HttpResponse<TV> httpResponse,
            IPageable pageable
        ) {
            return new HttpPageResponse<TV>(
                httpResponse.Retrieve(),
                Option.Some(pageable),
                httpResponse.StatusCode,
                httpResponse.Headers,
                httpResponse.Errors
            );
        }

        public static HttpPageResponse<TV> OfFailure(
            HttpResponse<TV> httpResponse,
            Option<IPageable> pageable,
            params IError[] errors
        ) {
            return new HttpPageResponse<TV>(
                httpResponse.Retrieve(),
                pageable,
                httpResponse.StatusCode,
                httpResponse.Headers,
                httpResponse.Errors.Merge(errors)
            );
        }

        public static HttpPageResponse<TV> OfFailure(
            TV body,
            IPageable pageable,
            HttpResponseMessage response,
            params IError[] errors
        ) {
            return new HttpPageResponse<TV>(
                body,
                pageable,
                response,
                errors.ToList()
            );
        }

        public static HttpPageResponse<TV> OfFailure(
            TV body,
            HttpResponseMessage response,
            params IError[] errors
        ) {
            return new HttpPageResponse<TV>(
                body,
                null,
                response,
                errors.ToList()
            );
        }

        public static HttpPageResponse<TV> OfFailure(
            HttpResponseMessage response,
            params IError[] errors
        ) {
            return new HttpPageResponse<TV>(
                null,
                null,
                response,
                errors.ToList()
            );
        }

        public static HttpPageResponse<TV> OfFailure(
            TV body,
            IPageable pageable,
            HttpResponseMessage response,
            IEnumerable<IError> errors
        ) {
            return new HttpPageResponse<TV>(
                body,
                pageable,
                response,
                errors
            );
        }

        public static HttpPageResponse<TV> OfFailure(
            TV body,
            HttpResponseMessage response,
            IEnumerable<IError> errors
        ) {
            return new HttpPageResponse<TV>(
                body,
                null,
                response,
                errors
            );
        }

        public static HttpPageResponse<TV> OfFailure(
            HttpResponseMessage response,
            IEnumerable<IError> errors
        ) {
            return new HttpPageResponse<TV>(
                null,
                null,
                response,
                errors
            );
        }
    }
}