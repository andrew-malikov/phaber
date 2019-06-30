using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Phaber.Infrastructure.Http;
using Phaber.Unsplash.Entities;
using Phaber.Unsplash.Models;

namespace Phaber.Unsplash.Clients {
    public class CollectionClient : ICollectionClient {
        private readonly ApiUris _endpoints;
        private readonly IConnection _connection;

        public CollectionClient(ApiUris endpoints, IConnection connection) {
            _endpoints = endpoints;
            _connection = connection;
        }

        public Task<IFallibleBodyResponse<Collection>> GetAsync(Collection collection) {
            return GetAsync(collection.Id);
        }

        public async Task<IFallibleBodyResponse<Collection>> GetAsync(string collectionId) {
            return await _connection.MakeRequest<Collection>(
                _endpoints.Collection(collectionId),
                HttpMethod.Get
            );
        }

        public IPageableResponse<IEnumerable<Photo>> GetPhotos(
            string collectionId,
            int page = 1,
            int perPage = 30
        ) {
            return _connection.MakePagedRequest<IEnumerable<Photo>>(
                _endpoints.CollectionPhotos(collectionId),
                page,
                perPage,
                HttpMethod.Get
            );
        }
    }
}