using System.Collections.Generic;

namespace Phaber.Unsplash.Helpers {
    public static class Aggregations {
        public static string Aggregate(
            this IEnumerable<string> context,
            string delimiter = ""
        ) {
            return string.Join(delimiter, context);
        }
    }
}