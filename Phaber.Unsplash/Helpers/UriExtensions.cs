using System;
using System.Collections.Generic;
using System.Web;

namespace Phaber.Unsplash.Helpers {
    public static class UriExtensions {
        public static Uri AddQueries(this Uri uri, IDictionary<string, string> queries) {
            var queriesFromUri = HttpUtility.ParseQueryString(uri.Query);

            foreach (var query in queries)
                queriesFromUri[query.Key] = query.Value;

            var uriWithQueries = new UriBuilder(uri);
            uriWithQueries.Query = queriesFromUri.ToString();

            return uriWithQueries.Uri;
        }
    }


}




