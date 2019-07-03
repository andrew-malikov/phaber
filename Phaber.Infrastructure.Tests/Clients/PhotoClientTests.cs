using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optional.Unsafe;
using Phaber.Infrastructure.Tests.Environment;
using Phaber.Unsplash.Clients;
using Phaber.Unsplash.Entities;

namespace Phaber.Infrastructure.Tests.Clients {
    [TestClass]
    public class PhotoClientTests {
        private readonly IPhotoClient _client;

        public PhotoClientTests() {
            _client = new EnvironmentHttpClientFabric().GetPhotoClient();
        }

        [TestMethod]
        public async Task ShouldReturnPhotoById() {
            var photoId = "sZey8cPbJqg";

            var foundPhoto = await _client.GetPhotoAsync(photoId);

            Assert.IsTrue(foundPhoto.IsSuccess);
            Assert.AreEqual(
                photoId,
                foundPhoto.Retrieve().ValueOrFailure().Id
            );
        }

        [TestMethod]
        public async Task ShouldReturnPhotoByModel() {
            var photoModel = new Photo { Id = "sZey8cPbJqg" };

            var foundPhoto = await _client.GetPhotoAsync(photoModel);

            Assert.IsTrue(foundPhoto.IsSuccess);
            Assert.AreEqual(
                photoModel,
                foundPhoto.Retrieve().ValueOrFailure()
            );
        }

        [TestMethod]
        public async Task ShouldReturnDownloadLinkByPhotoId() {
            var photoId = "sZey8cPbJqg";

            var downloadLink = await _client.GetDownloadLinkAsync(photoId);

            Assert.IsTrue(downloadLink.IsSuccess);
            StringAssert.Matches(
                downloadLink.Retrieve().ValueOrFailure().ToString(),
                new Regex("^.+$")
            );
        }

        [TestMethod]
        public async Task ShouldReturnStreamOfPhotoContentByPhotoId() {
            var contentToFetch = await _client.FetchPhotoContentStreamAsync("sZey8cPbJqg");

            Assert.IsTrue(contentToFetch.IsSuccess);

            var fetchedContent = new StreamReader(
                contentToFetch.Retrieve().ValueOrFailure()
            );

            Assert.IsTrue(fetchedContent.ReadToEnd().Length > 0);
        }
    }
}
