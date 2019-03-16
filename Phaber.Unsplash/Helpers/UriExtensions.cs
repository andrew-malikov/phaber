using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// ATTENTION: USE ONLY WITH RELATIVE URIS
        /// 
        /// Combines the relative uri base and the relative uri into one, consolidating the '/' between them
        /// </summary>
        /// <param name="urlBase">Base uri that will be combined</param>
        /// <param name="relativeUri">The relative path to combine</param>
        /// <returns>The merged uri</returns>
        public static Uri Resolve(this Uri baseUri, string relativeUri) {
            if (string.IsNullOrWhiteSpace(relativeUri))
                return baseUri;

            var trimmedBaseUri = baseUri.LocalPath.ToString().TrimEnd('/');
            var trimmedRelativeUri = relativeUri.TrimStart('/');

            return new Uri($"{trimmedBaseUri}/{trimmedRelativeUri}", UriKind.Relative);
        }

        /// <summary>
        /// ATTENTION: USE ONLY WITH RELATIVE URIS
        /// 
        /// Combines the relative uri base and the array of relatives uris into one, consolidating the '/' between them
        /// </summary>
        /// <param name="urlBase">Base uri that will be combined</param>
        /// <param name="relativeUrl">The array of relative paths to combine</param>
        /// <returns>The merged uri</returns>
        public static Uri Resolve(this Uri baseUri, params string[] relativePaths) {
            if (relativePaths.Length == 0)
                return baseUri;

            var currentUri = Resolve(baseUri, relativePaths[0]);

            return Resolve(currentUri, relativePaths.Skip(1).ToArray());
        }
    }
}