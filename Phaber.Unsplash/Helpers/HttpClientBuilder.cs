using System;
using System.Net.Http;

namespace Phaber.Unsplash.Helpers {
    public class HttpClientBuilder {
        private readonly HttpClient _client;

        public HttpClientBuilder(HttpClient client) {
            _client = client;
        }

        public HttpClientBuilder SetDomain(Uri domain) {
            _client.BaseAddress = domain;

            return this;
        }

        public HttpClientBuilder SetTimeout(TimeSpan timeout) {
            _client.Timeout = timeout;

            return this;
        }

        public HttpClient Done => _client;
    }
}