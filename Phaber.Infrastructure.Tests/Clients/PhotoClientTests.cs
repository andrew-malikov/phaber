using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Optional.Unsafe;
using Phaber.Infrastructure.Tests.Environment;
using Phaber.Unsplash.Clients;
using Phaber.Unsplash.Entities;
using Xunit;

namespace Phaber.Infrastructure.Tests.Clients {
    public class PhotoClientTests {
        private readonly IPhotoClient _client;

        public PhotoClientTests() {
            _client = new EnvironmentHttpClientFabric().GetPhotoClient();
        }

        [Fact]
        public async Task ShouldReturnPhotoById() {
            var photoId = "sZey8cPbJqg";

            var foundPhoto = await _client.GetPhotoAsync(photoId);

            Assert.True(foundPhoto.IsSuccess);
            Assert.Equal(
                photoId,
                foundPhoto.Retrieve().ValueOrFailure().Id
            );
        }

        [Fact]
        public async Task ShouldReturnPhotoByModel() {
            var photoModel = new Photo { Id = "sZey8cPbJqg" };

            var foundPhoto = await _client.GetPhotoAsync(photoModel);

            Assert.True(foundPhoto.IsSuccess);
            Assert.Equal(
                photoModel,
                foundPhoto.Retrieve().ValueOrFailure()
            );
        }

        [Fact]
        public async Task ShouldReturnDownloadLinkByPhotoId() {
            var photoId = "sZey8cPbJqg";

            var downloadLink = await _client.GetDownloadLinkAsync(photoId);

            Assert.True(downloadLink.IsSuccess);
            Assert.Matches(
                new Regex("^.+$"),
                downloadLink.Retrieve().ValueOrFailure().ToString()
            );
        }

        [Fact]
        public async Task ShouldReturnStreamOfPhotoContentByPhotoId() {
            var contentToFetch = await _client.FetchPhotoContentStreamAsync("sZey8cPbJqg");

            Assert.True(contentToFetch.IsSuccess);

            var fetchedContent = new StreamReader(
                contentToFetch.Retrieve().ValueOrFailure()
            );

            Assert.True(fetchedContent.ReadToEnd().Length > 0);
        }
    }
}
