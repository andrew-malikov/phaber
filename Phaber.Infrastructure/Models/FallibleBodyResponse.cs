using System;
using System.Collections.Generic;
using System.Linq;
using Phaber.Infrastructure.Errors;
using Phaber.Unsplash.Models;

namespace Phaber.Infrastructure.Models {
    public class FallibleBodyResponse<TV> : IFallibleBodyResponse<TV> where TV : class {
        public IEnumerable<Error> Errors { get; }
        public bool IsSuccess => !Errors.Any() && _valuable != null;

        private readonly TV _valuable;

        private FallibleBodyResponse(TV valuable, IEnumerable<Error> errors) {
            Errors = errors;

            _valuable = valuable;
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

        public TV Retrieve() {
            if (_valuable != null) {
                return _valuable;
            }

            throw new InvalidOperationException();
        }
    }
}