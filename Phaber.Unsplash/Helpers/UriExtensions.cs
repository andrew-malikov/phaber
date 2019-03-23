using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phaber.Unsplash.Helpers {
    public static class UriExtensions {
        /// <summary>
        ///  Adds quries to absolute uri
        /// </summary>
        /// <param name="uri">The absolute uri</param>
        /// <param name="queries">Queries to add</param>
        /// <returns>The absolute uri with queries</returns>
        public static Uri AddQueries(
            this Uri uri,
            IDictionary<string, string> queries
        ) {
            if (!uri.IsAbsoluteUri)
                throw new ArgumentException($"can't work with relative uri: {uri}");

            var queriesFromUri = HttpUtility.ParseQueryString(uri.Query);

            foreach (var query in queries)
                queriesFromUri[query.Key] = query.Value;

            var uriWithQueries = new UriBuilder(uri.ToString().TrimEnd('/'));
            uriWithQueries.Query = queriesFromUri.ToString();

            return uriWithQueries.Uri;
        }

        public static Uri Resolve(this Uri baseUri, string relativeUri) {
            return new Uri(baseUri, relativeUri);
        }

        /// <summary>
        /// Combines the relative uri base and the relative uri into one, consolidating the '/' between them
        /// </summary>
        /// <param name="urlBase">Base uri that will be combined</param>
        /// <param name="relativeUri">The relative path to combine</param>
        /// <returns>The merged uri</returns>
        public static Uri ResolveRelative(this Uri baseUri, string relativeUri) {
            if (string.IsNullOrWhiteSpace(relativeUri))
                return baseUri;

            var trimmedBaseUri = baseUri.ToString().TrimEnd('/');
            var trimmedRelativeUri = relativeUri.TrimStart('/');

            return new Uri($"{trimmedBaseUri}/{trimmedRelativeUri}", UriKind.Relative);
        }

        /// <summary>
        /// Combines the relative uri base and the array of relatives uris into one, consolidating the '/' between them
        /// </summary>
        /// <param name="urlBase">Base uri that will be combined</param>
        /// <param name="relativeUrl">The array of relative paths to combine</param>
        /// <returns>The merged uri</returns>
        public static Uri ResolveRelative(this Uri baseUri, params string[] relativePaths) {
            if (relativePaths.Length == 0)
                return baseUri;

            var currentUri = ResolveRelative(baseUri, relativePaths[0]);

            return ResolveRelative(currentUri, relativePaths.Skip(1).ToArray());
        }
    }
}