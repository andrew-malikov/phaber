using System.Collections.Generic;
using System.Net;

namespace Phaber.Infrastructure.Errors {
    public class HttpError : Error {
        public readonly HttpStatusCode StatusCode;
        public readonly Dictionary<string, string> Headers;

        public HttpError(
            string debugMessage,
            string message,
            HttpStatusCode statusCode,
            Dictionary<string, string> headers
        ) : base(debugMessage, message) {
            StatusCode = statusCode;
            Headers = headers;
        }
    }
}