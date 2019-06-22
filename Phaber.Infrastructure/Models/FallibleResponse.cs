using System.Collections.Generic;
using System.Linq;
using Phaber.Infrastructure.Errors;
using Phaber.Unsplash.Models;

namespace Phaber.Infrastructure.Models {
    public class FallibleResponse : IFallibleResponse {
        public IEnumerable<Error> Errors { get; }
        public bool IsSuccess => !Errors.Any();

        private FallibleResponse(IEnumerable<Error> errors) {
            Errors = errors;
        }

        public static FallibleResponse OfSuccessful() {
            return new FallibleResponse(new List<Error>());
        }

        public static FallibleResponse OfFailure(params Error[] errors) {
            return new FallibleResponse(errors.ToList());
        }
    }
}