using Phaber.Unsplash;
using Phaber.Unsplash.Errors.Domain;

namespace Phaber.Infrastructure.Errors.Domain {
    public class HttpRateLimitError : ValuableError<RateLimit>, IRateLimitError {
        public HttpRateLimitError(
            RateLimit valuable,
            string debugMessage = "",
            string message = "client rate limit was exceeded"
        ) : base(debugMessage, message, valuable) { }
    }
}