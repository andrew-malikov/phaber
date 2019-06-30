using Optional;
using Phaber.Infrastructure.Errors;
using Phaber.Unsplash.Errors;
using Phaber.Unsplash.Models;

namespace Phaber.Infrastructure.Models {
    public static class FallibleModelTransformer {
        public static IFallibleResponse Convert<TV>(
            this IFallibleBodyResponse<TV> bodyResponse,
            params IError[] errors
        ) {
            return FallibleResponse.OfFailure(
                bodyResponse.Errors.Merge(errors)
            );
        }

        public static IFallibleBodyResponse<TV> Convert<TV>(
            this IFallibleResponse response,
            Option<TV> body,
            params IError[] errors
        ) where TV : class {
            return FallibleBodyResponse<TV>.OfFailure(
                body,
                response.Errors.Merge(errors)
            );
        }
    }
}