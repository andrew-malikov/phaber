using System.Net;
using System.Net.Http;

using Phaber.Unsplash.Exceptions;

namespace Phaber.Unsplash.Http {
    public class RateLimitValidationHandler : IValidatableHttpResponse {
        public void Handle(HttpResponseMessage response) {
            if (HasForbiddenStatusCode(response) && HasOfficialRateLimitMessage(response))
                throw new RateLimitExceededException(new RateLimit(response));
        }

        protected bool HasForbiddenStatusCode(HttpResponseMessage response) {
            return response.StatusCode == HttpStatusCode.Forbidden;
        }

        protected bool HasOfficialRateLimitMessage(HttpResponseMessage response) {
            return response.Content.ReadAsStringAsync().Result.ToLower() == "rate limit exceeded";
        }
    }
}