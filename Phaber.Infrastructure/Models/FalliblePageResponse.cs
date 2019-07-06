using System.Collections.Generic;
using System.Linq;
using Optional;
using Phaber.Unsplash.Errors;
using Phaber.Unsplash.Models;

namespace Phaber.Infrastructure.Models {
    public class FalliblePageResponse<TV> : FallibleBodyResponse<TV>, IFalliblePageResponse<TV> where TV : class {
        public Option<IPageable> Pageable { get; private set; }

        protected FalliblePageResponse(
            TV valuable,
            IPageable pageable,
            IEnumerable<IError> errors
        ) : this(
            valuable != null ? Option.Some(valuable) : Option.None<TV>(),
            pageable != null ? Option.Some(pageable) : Option.None<IPageable>(),
            errors
        ) { }

        protected FalliblePageResponse(
            Option<TV> valuable,
            Option<IPageable> pageable,
            IEnumerable<IError> errors
        ) : base(valuable, errors) {
            Pageable = pageable;
        }

        public static FalliblePageResponse<TV> OfSuccessful(TV body, IPageable pageable) {
            return new FalliblePageResponse<TV>(
                body,
                pageable,
                new List<IError>()
            );
        }

        public static FalliblePageResponse<TV> OfFailure(
            TV body,
            IPageable pageable,
            params IError[] errors
        ) {
            return new FalliblePageResponse<TV>(
                body,
                pageable,
                errors.ToList()
            );
        }

        public static FalliblePageResponse<TV> OfFailure(
            TV body,
            params IError[] errors
        ) {
            return new FalliblePageResponse<TV>(
                body,
                null,
                errors.ToList()
            );
        }

        public static FalliblePageResponse<TV> OfFailure(params IError[] errors) {
            return new FalliblePageResponse<TV>(
                null,
                null,
                errors.ToList()
            );
        }

        public static FalliblePageResponse<TV> OfFailure(
            TV body,
            IPageable pageable,
            IEnumerable<IError> errors
        ) {
            return new FalliblePageResponse<TV>(
                body,
                pageable,
                errors
            );
        }

        public static FalliblePageResponse<TV> OfFailure(
            TV body,
            IEnumerable<IError> errors
        ) {
            return new FalliblePageResponse<TV>(
                body,
                null,
                errors
            );
        }

        public static FalliblePageResponse<TV> OfFailure(IEnumerable<IError> errors) {
            return new FalliblePageResponse<TV>(
                null,
                null,
                errors
            );
        }
    }
}