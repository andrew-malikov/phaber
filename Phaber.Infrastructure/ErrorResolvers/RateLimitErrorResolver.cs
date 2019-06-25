using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Phaber.Infrastructure.Errors.Domain;
using Phaber.Unsplash;
using Phaber.Unsplash.Errors;

namespace Phaber.Infrastructure.ErrorResolvers {
    public class RateLimitErrorResolver : IErrorResolver<HttpResponseMessage> {
        public IEnumerable<IError> Resolve(HttpResponseMessage response) {
            if (HasForbiddenStatusCode(response) && HasOfficialRateLimitMessage(response)) {
                return new List<IError> {new HttpRateLimitError(new RateLimit(response))};
            }

            return new List<IError>();
        }

        protected bool HasForbiddenStatusCode(HttpResponseMessage response) {
            return response.StatusCode == HttpStatusCode.Forbidden;
        }

        protected bool HasOfficialRateLimitMessage(HttpResponseMessage response) {
            return response.Content.ReadAsStringAsync().Result.ToLower() == "rate limit exceeded";
        }
    }
}