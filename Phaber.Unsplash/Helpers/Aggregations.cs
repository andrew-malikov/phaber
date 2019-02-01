using System;
using System.Collections.Generic;
using System.Linq;

namespace Phaber.Unsplash.Helpers {
    public static class Aggregations {
        public static string Aggregate(
            this IEnumerable<string> context,
            string delimiter = ""
        ) {
            return String.Join(delimiter, context);
        }
    }
}