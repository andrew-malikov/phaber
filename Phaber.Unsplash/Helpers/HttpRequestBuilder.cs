using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Phaber.Unsplash.Helpers {
    public class HttpRequestBuilder {
        private readonly HttpRequestMessage _request;

        public HttpRequestBuilder(HttpRequestMessage request) {
            _request = request;
        }

        public HttpRequestBuilder SetMethod(HttpMethod method) {
            _request.Method = method;

            return this;
        }

        public HttpRequestBuilder SetEndpoint(Uri endpoint) {
            _request.RequestUri = endpoint;

            return this;
        }

        public HttpRequestBuilder SetContent(HttpContent content) {
            _request.Content = content;

            return this;
        }

        public HttpRequestBuilder AddHeaders(Dictionary<string, string> headers) {
            foreach (var header in headers)
                _request.Headers.Add(header.Key, header.Value);

            return this;
        }

        public HttpRequestMessage Done => _request;
    }
}