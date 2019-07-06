using System.Collections.Generic;
using System.Linq;
using Optional;
using Phaber.Unsplash.Errors;
using Phaber.Unsplash.Models;

namespace Phaber.Infrastructure.Models {
    public class FallibleBodyResponse<TV> : IFallibleBodyResponse<TV> where TV : class {
        public IEnumerable<IError> Errors { get; }
        public bool IsSuccess => !Errors.Any() && _valuable.HasValue;

        private readonly Option<TV> _valuable;

        protected FallibleBodyResponse(
            TV valuable,
            IEnumerable<IError> errors
        ) : this(
            valuable != null ? Option.Some(valuable) : Option.None<TV>(),
            errors
        ) { }

        protected FallibleBodyResponse(
            Option<TV> valuable,
            IEnumerable<IError> errors
        ) {
            _valuable = valuable;
            Errors = errors;
        }

        public static FallibleBodyResponse<TV> OfSuccessful(TV body) {
            return new FallibleBodyResponse<TV>(
                body,
                new List<IError>()
            );
        }

        public static FallibleBodyResponse<TV> OfFailure(
            Option<TV> body,
            params IError[] errors
        ) {
            return new FallibleBodyResponse<TV>(
                body,
                errors.ToList()
            );
        }

        public static FallibleBodyResponse<TV> OfFailure(
            TV body,
            params IError[] errors
        ) {
            return new FallibleBodyResponse<TV>(
                body,
                errors.ToList()
            );
        }

        public static FallibleBodyResponse<TV> OfFailure(params IError[] errors) {
            return new FallibleBodyResponse<TV>(
                null,
                errors.ToList()
            );
        }

        public static FallibleBodyResponse<TV> OfFailure(
            Option<TV> body,
            IEnumerable<IError> errors
        ) {
            return new FallibleBodyResponse<TV>(
                body,
                errors
            );
        }

        public static FallibleBodyResponse<TV> OfFailure(
            TV body,
            IEnumerable<IError> errors
        ) {
            return new FallibleBodyResponse<TV>(
                body,
                errors
            );
        }

        public static FallibleBodyResponse<TV> OfFailure(IEnumerable<IError> errors) {
            return new FallibleBodyResponse<TV>(
                null,
                errors
            );
        }

        public Option<TV> Retrieve() {
            return _valuable;
        }
    }
}