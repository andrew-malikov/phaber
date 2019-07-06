using Phaber.Infrastructure.Http;
using Phaber.Unsplash;
using Phaber.Unsplash.Clients;

namespace Phaber.Infrastructure.Fabrics {
    public class HttpClientFabric : IClientFabric {
        private readonly Credentials _credentials;

        public HttpClientFabric(Credentials credentials) {
            _credentials = credentials;
        }

        public IPhotoClient GetPhotoClient() {
            return new PhotoClient(
                new ApiUris(),
                new HttpConnection(_credentials)
            );
        }

        public ICollectionClient GetCollectionClient() {
            return new CollectionClient(
                new ApiUris(),
                new HttpConnection(_credentials)
            );
        }
    }
}