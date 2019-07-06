using System.Linq;
using System.Threading.Tasks;
using Optional.Unsafe;
using Phaber.Infrastructure.Tests.Environment;
using Phaber.Unsplash.Clients;
using Phaber.Unsplash.Entities;
using Xunit;

namespace Phaber.Infrastructure.Tests.Clients {
    public class CollectionClientTests {
        private readonly ICollectionClient _client;

        public CollectionClientTests() {
            _client = new EnvironmentHttpClientFabric().GetCollectionClient();
        }

        [Fact]
        public async Task ShouldReturnCollectionById() {
            var collectionId = "4807737";

            var foundCollection = await _client.GetAsync(collectionId);

            Assert.True(foundCollection.IsSuccess);
            Assert.Equal(
                collectionId,
                foundCollection.Retrieve().ValueOrFailure().Id
            );
        }

        [Fact]
        public async Task ShouldReturnCollectionByModel() {
            var collectionModel = new Collection {Id = "4807737"};

            var foundCollection = await _client.GetAsync(collectionModel);

            Assert.True(foundCollection.IsSuccess);
            Assert.Equal(
                collectionModel,
                foundCollection.Retrieve().ValueOrFailure()
            );
        }

        [Fact]
        public void ShouldReturnPhotosByCollectionId() {
            var collectionId = "4807737";

            var pageablePhotos = _client.GetPhotos(collectionId, 1, 15);

            foreach (var photos in pageablePhotos) {
                Assert.True(photos.IsSuccess);
                Assert.True(photos
                    .Retrieve()
                    .ValueOrFailure()
                    .Any()
                );
            }
        }
    }
}