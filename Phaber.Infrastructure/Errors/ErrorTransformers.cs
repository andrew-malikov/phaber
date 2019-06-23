using System;
using System.Collections.Generic;
using System.Linq;

namespace Phaber.Infrastructure.Errors {
    public static class ErrorTransformers {
        public static IEnumerable<Error> FilterByTypes(
            this IEnumerable<Error> errors,
            params Type[] types
        ) {
            var extendedTypes = types.ToList();

            return errors
                .Where(
                    error => extendedTypes.Contains(error.GetType())
                );
        }

        public static IEnumerable<TE> FilterByType<TE>(
            this IEnumerable<Error> errors
        ) where TE : Error{
            return (IEnumerable<TE>) errors
                .Where(
                    error => error.GetType() == typeof(TE)
                );
        }
    }
}