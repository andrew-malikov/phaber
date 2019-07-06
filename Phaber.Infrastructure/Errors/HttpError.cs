using System.Collections.Generic;
using System.Net;

namespace Phaber.Infrastructure.Errors {
    public class HttpError : Error {
        public readonly HttpStatusCode StatusCode;
        public readonly IDictionary<string, string> Headers;

        public HttpError(
            string debugMessage,
            string message,
            HttpStatusCode statusCode,
            IDictionary<string, string> headers
        ) : base(debugMessage, message) {
            StatusCode = statusCode;
            Headers = headers;
        }
    }
}