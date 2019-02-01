using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Phaber.Unsplash.Http;
using Phaber.Unsplash.Models;

namespace Phaber.Unsplash.Clients {
    public class CollectionClient {
        private readonly ApiUrls _endpoints;
        private readonly Connection _connection;

        public CollectionClient(ApiUrls endpoints, Connection connection) {
            _endpoints = endpoints;
            _connection = connection;
        }

        public Task<Collection> GetAsync(Collection collection) {
            return GetAsync(collection.Id);
        }

        public async Task<Collection> GetAsync(string collectionId) {
            return (await _connection.MakeRequest<Collection>(
                _endpoints.Domain,
                _endpoints.Collection(collectionId),
                HttpMethod.Get,
                async body => JsonConvert.DeserializeObject<Collection>(
                    await body.AsStringAsync()
                )
            )).Body;
        }

        public PagedResponse<List<Photo>> GetPhotosAsync(
            string collectionId,
            int page = 1,
            int perPage = 30
        ) {
            return _connection.MakePagedRequest<List<Photo>>(
                _endpoints.Domain,
                _endpoints.CollectionPhotos(collectionId),
                page,
                perPage,
                HttpMethod.Get,
                async body => JsonConvert.DeserializeObject<List<Photo>>(
                    await body.AsStringAsync()
                )
            );
        }
    }
}