using System.Collections.Generic;
using System.Linq;
using Phaber.Infrastructure.Errors;
using Phaber.Unsplash.Errors;
using Phaber.Unsplash.Models;

namespace Phaber.Infrastructure.Models {
    public class FallibleResponse : IFallibleResponse {
        public IEnumerable<IError> Errors { get; }
        public bool IsSuccess => !Errors.Any();

        protected FallibleResponse(IEnumerable<IError> errors) {
            Errors = errors;
        }

        public static FallibleResponse OfSuccessful() {
            return new FallibleResponse(new List<IError>());
        }

        public static FallibleResponse OfFailure(params IError[] errors) {
            return new FallibleResponse(errors.ToList());
        }

        public static FallibleResponse OfFailure(IEnumerable<IError> errors) {
            return new FallibleResponse(errors);
        }
    }
}