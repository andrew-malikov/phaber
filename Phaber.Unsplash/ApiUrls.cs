using System;
using System.Collections;
using System.Collections.Generic;

using Phaber.Unsplash.Helpers;

namespace Phaber.Unsplash {
    public class ApiUrls {
        public readonly Uri Domain;
        private readonly Dictionary<string, Uri> _endpoints;

        public Uri Photos => _endpoints["photos"];
        public Uri Photo(string id) => new Uri(Photos, $"/{id}");
        public Uri Photo(string id, int width, int height) {
            return new Uri(Photos, $"/{id}").AddQueries(
                new Dictionary<string, string> {
                    {"w", $"{width}"},
                    {"h", $"{height}"}
                }
            );
        }
        public Uri PhotoDownloadLink(string id) => new Uri(Photos, $"/{id}/download");

        public Uri CuratedPhotos => _endpoints["curated_photos"];

        public Uri Search => _endpoints["search"];

        public Uri Users => _endpoints["users"];

        public Uri Collections => _endpoints["collections"];
        public Uri Collection(string id) => new Uri(Collections, $"/{id}");
        public Uri CollectionPhotos(string id) => new Uri(Collection(id), "/photos");


        public Uri SearchPhotos => _endpoints["search_photos"];

        public Uri SearchCollections => _endpoints["search_collections"];

        public Uri SearchUsers => _endpoints["search_users"];

        public Uri TotalStats => _endpoints["total_stats"];

        public Uri MonthlyStats => _endpoints["monthly_stats"];

        public ApiUrls(Uri domain, Dictionary<string, Uri> endpoints) {
            Domain = domain;
            _endpoints = endpoints;
        }

        public ApiUrls() : this(
            new Uri("https://api.unsplash.com/"),
             new Dictionary<string, Uri>() {
                {"photos", new Uri("photos") },
                {"curated_photos", new Uri("photos/curated") },
                {"search", new Uri("search") },
                {"users", new Uri("users") },
                {"collections", new Uri("collections") },
                {"search_photos", new Uri("search/photos") },
                {"search_collections", new Uri("search/collections") },
                {"search_users", new Uri("search/users") },
                {"total_stats", new Uri("stats/total") },
                {"monthly_stats", new Uri("stats/month") }
            }
        ) { }
    }
}