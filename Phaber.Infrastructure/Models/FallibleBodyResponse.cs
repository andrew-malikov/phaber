using System.Collections.Generic;
using System.Linq;
using Optional;
using Phaber.Infrastructure.Errors;
using Phaber.Unsplash.Models;

namespace Phaber.Infrastructure.Models {
    public class FallibleBodyResponse<TV> : IFallibleBodyResponse<TV> where TV : class {
        public IEnumerable<Error> Errors { get; }
        public bool IsSuccess => !Errors.Any() && _valuable.HasValue;

        private readonly Option<TV> _valuable;

        private FallibleBodyResponse(TV valuable, IEnumerable<Error> errors) {
            Errors = errors;

            _valuable = valuable != null ? Option.Some(valuable) : Option.None<TV>();
        }

        public static FallibleBodyResponse<TV> OfSuccessful(TV body) {
            return new FallibleBodyResponse<TV>(
                body,
                new List<Error>()
            );
        }

        public static FallibleBodyResponse<TV> OfFailure(TV body, params Error[] errors) {
            return new FallibleBodyResponse<TV>(
                body,
                errors.ToList()
            );
        }

        public static FallibleBodyResponse<TV> OfFailure(params Error[] errors) {
            return new FallibleBodyResponse<TV>(
                null,
                errors.ToList()
            );
        }

        public Option<TV> Retrieve() {
            return _valuable;
        }
    }
}