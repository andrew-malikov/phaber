using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Phaber.Infrastructure.Errors;
using Phaber.Unsplash.Errors;
using Phaber.Unsplash.Helpers;

namespace Phaber.Infrastructure.ErrorResolvers {
    public class UnauthenticatedErrorResolver : IErrorResolver<HttpResponseMessage> {
        public IEnumerable<IError> Resolve(HttpResponseMessage resolvable) {
            var errors = new List<IError>();

            if (resolvable.StatusCode == HttpStatusCode.Unauthorized) {
                errors.Add(new HttpError(
                        resolvable.Content.ReadAsStringAsync().Result,
                        "troubles in the authentication process",
                        resolvable.StatusCode,
                        resolvable.Headers.Aggregate()
                    )
                );
            }

            return errors;
        }
    }
}