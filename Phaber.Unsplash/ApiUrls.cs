using System;
using System.Collections;
using System.Collections.Generic;

using Phaber.Unsplash.Helpers;

namespace Phaber.Unsplash {
    public class ApiUrls {
        public readonly Uri Domain;
        private readonly Dictionary<string, Uri> _endpoints;

        public Uri Photos = new Uri("photos");
        public Uri Photo(string id) => Photos.Resolve($"/{id}");
        public Uri Photo(string id, int width, int height) {
            return Photos.Resolve($"/{id}").AddQueries(
                new Dictionary<string, string> {
                    {"w", $"{width}"},
                    {"h", $"{height}"}
                }
            );
        }
        public Uri PhotoDownloadLink(string id) => Photos.Resolve($"/{id}/download");

        public Uri CuratedPhotos = new Uri("photos/curated");

        public Uri Search = new Uri("search");

        public Uri Users = new Uri("users");

        public Uri Collections = new Uri("collections");
        public Uri Collection(string id) => Collections.Resolve($"/{id}");
        public Uri CollectionPhotos(string id) => Collection(id).Resolve("/photos");


        public Uri SearchPhotos = new Uri("search/photos");

        public Uri SearchCollections = new Uri("search/collections");

        public Uri SearchUsers = new Uri("search/users");

        public Uri TotalStats = new Uri("stats/total");

        public Uri MonthlyStats = new Uri("stats/month");

        public ApiUrls(Uri domain) {
            Domain = domain;
        }

        public ApiUrls() : this(new Uri("https://api.unsplash.com/")) { }
    }
}