using System;
using System.Collections.Generic;
using System.Linq;
using Phaber.Unsplash.Errors;

namespace Phaber.Infrastructure.Errors {
    public static class ErrorTransformers {
        public static IEnumerable<IError> FilterByTypes(
            this IEnumerable<IError> errors,
            params Type[] types
        ) {
            var extendedTypes = types.ToList();

            return errors
                .Where(
                    error => extendedTypes.Contains(error.GetType())
                );
        }

        public static IEnumerable<TE> FilterByType<TE>(
            this IEnumerable<IError> errors
        ) where TE : IError{
            return (IEnumerable<TE>) errors
                .Where(
                    error => error.GetType() == typeof(TE)
                );
        }
    }
}