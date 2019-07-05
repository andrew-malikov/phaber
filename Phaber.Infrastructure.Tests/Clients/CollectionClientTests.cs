using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optional.Unsafe;
using Phaber.Infrastructure.Tests.Environment;
using Phaber.Unsplash.Clients;
using Phaber.Unsplash.Entities;

namespace Phaber.Infrastructure.Tests.Clients {
    [TestClass]
    public class CollectionClientTests {
        private readonly ICollectionClient _client;

        public CollectionClientTests() {
            _client = new EnvironmentHttpClientFabric().GetCollectionClient();
        }

        [TestMethod]
        public async Task ShouldReturnCollectionById() {
            var collectionId = "4807737";

            var foundCollection = await _client.GetAsync(collectionId);

            Assert.IsTrue(foundCollection.IsSuccess);
            Assert.AreEqual(
                collectionId,
                foundCollection.Retrieve().ValueOrFailure().Id
            );
        }

        [TestMethod]
        public async Task ShouldReturnCollectionByModel() {
            var collectionModel = new Collection {Id = "4807737"};

            var foundCollection = await _client.GetAsync(collectionModel);

            Assert.IsTrue(foundCollection.IsSuccess);
            Assert.AreEqual(
                collectionModel,
                foundCollection.Retrieve().ValueOrFailure()
            );
        }

        [TestMethod]
        public void ShouldReturnPhotosByCollectionId() {
            var collectionId = "4807737";

            var pageablePhotos = _client.GetPhotos(collectionId, 1, 15);

            foreach (var photos in pageablePhotos) {
                Assert.IsTrue(photos.IsSuccess);
                Assert.IsTrue(photos
                    .Retrieve()
                    .ValueOrFailure()
                    .Any()
                );
            }
        }
    }
}