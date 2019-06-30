using System;
using System.Collections.Generic;

using Phaber.Unsplash.Helpers;

namespace Phaber.Unsplash {
    public class ApiUris {
        public readonly Uri Domain;

        public readonly Uri Photos;
        public Uri Photo(string id) => Photos.Resolve($"{id}/");
        public Uri Photo(string id, int width, int height) {
            return Photo(id).AddQueries(
                new Dictionary<string, string> {
                    {"w", $"{width}"},
                    {"h", $"{height}"}
                }
            );
        }
        public Uri PhotoDownloadLink(string id) => Photo(id).Resolve("download/");

        public readonly Uri CuratedPhotos;

        public readonly Uri Search;

        public readonly Uri Users;

        public readonly Uri Collections;
        public Uri Collection(string id) => Collections.Resolve($"{id}/");
        public Uri CollectionPhotos(string id) => Collection(id).Resolve("photos/");

        public readonly Uri SearchPhotos;

        public readonly Uri SearchCollections;

        public readonly Uri SearchUsers;

        public readonly Uri TotalStats;

        public readonly Uri MonthlyStats;

        public ApiUris(Uri domain) {
            Domain = domain;

            Photos = Domain.Resolve("photos/");
            CuratedPhotos = Domain.Resolve("photos/curated/");
            Search = Domain.Resolve("search/");
            Users = Domain.Resolve("users/");
            Collections = Domain.Resolve("collections/");
            SearchPhotos = Domain.Resolve("search/photos/");
            SearchCollections = Domain.Resolve("search/collections/");
            SearchUsers = Domain.Resolve("search/users/");
            TotalStats = Domain.Resolve("stats/total/");
            MonthlyStats = Domain.Resolve("stats/month/");
        }

        public ApiUris() : this(new Uri("https://api.unsplash.com/")) { }
    }
}