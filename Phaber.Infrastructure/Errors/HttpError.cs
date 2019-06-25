using System.Net;
using System.Net.Http.Headers;
using Phaber.Unsplash.Errors;

namespace Phaber.Infrastructure.Errors {
    public class HttpError : IError {
        public readonly HttpStatusCode StatusCode;
        public readonly HttpHeaders Headers;

        public string Message { get; private set; }

        public HttpError(
            string message,
            HttpStatusCode statusCode,
            HttpHeaders headers
        ) {
            StatusCode = statusCode;
            Headers = headers;
            Message = message;
        }
    }
}