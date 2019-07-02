using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Phaber.Unsplash.Helpers {
    public static class Aggregations {
        public static string Aggregate(
            this IEnumerable<string> context,
            string delimiter = ""
        ) {
            return string.Join(delimiter, context);
        }

        /// <param name="headers"></param>
        /// <param name="delimiter">separate the each of header values</param>
        public static IDictionary<string, string> Aggregate(
            this HttpHeaders headers,
            string delimiter = ""
        ) {
            return headers.ToDictionary(h => h.Key, h => h.Value.Aggregate(delimiter));
        }
    }
}