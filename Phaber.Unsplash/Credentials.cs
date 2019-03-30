using System.Collections.Generic;
using System.Net.Http;

namespace Phaber.Unsplash {
    public class Credentials {
        /// <summary>
        /// API Key 
        /// </summary>
        public readonly string Secret;

        /// <summary>
        /// Identify the application making API calls
        /// Must be created through Unsplash developer page
        /// https://unsplash.com/developers
        /// </summary>
        public readonly string ApplicationId;

        public Credentials(string appId, string secret) {
            ApplicationId = appId;
            Secret = secret;
        }

        public HttpRequestMessage Apply(HttpRequestMessage request) {
            request.Headers.Add(
                "Authorization", $"Client-ID {ApplicationId}"
            );

            return request;
        }
    }
}