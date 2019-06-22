using System.Net;
using System.Net.Http.Headers;

namespace Phaber.Infrastructure.Errors {
    public class HttpError : Error {
        public readonly HttpStatusCode StatusCode;
        public readonly HttpHeaders Headers;

        public HttpError(
            string message,
            HttpStatusCode statusCode,
            HttpHeaders headers
        ) : base(message) {
            StatusCode = statusCode;
            Headers = headers;
        }
    }
}