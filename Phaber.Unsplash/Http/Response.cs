using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using Phaber.Unsplash.Helpers;

namespace Phaber.Unsplash.Http {
    public class Response<T> {
        public readonly T Body;

        public readonly HttpStatusCode StatusCode;
        public readonly Dictionary<string, string> Headers;

        private readonly Lazy<RateLimit> _rateLimit;
        public RateLimit RateLimit => _rateLimit.Value;

        public Response(T body, HttpResponseMessage httpResponse) {
            Body = body;

            StatusCode = httpResponse.StatusCode;
            Headers = httpResponse
                .Headers
                .ToDictionary(h => h.Key, h => h.Value.Aggregate());

            _rateLimit = new Lazy<RateLimit>(
                () => new RateLimit(Headers)
            );
        }
    }
}