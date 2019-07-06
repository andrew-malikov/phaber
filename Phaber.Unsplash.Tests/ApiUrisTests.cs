using System;
using Xunit;

namespace Phaber.Unsplash.Tests {
    public class ApiUrisTests {
        private ApiUris _apiUris;

        public ApiUrisTests() {
            _apiUris = new ApiUris(
                new Uri("https://api.unsplash.com/")
            );
        }

        [Fact]
        public void ShouldReturnPhotoUriById() {
            Assert.Equal(
                "https://api.unsplash.com/photos/92148291/",
                _apiUris.Photo("92148291").ToString()
            );
        }

        [Fact]
        public void ShouldReturnPhotoUriByIdWithResolution() {
            Assert.Equal(
                "https://api.unsplash.com/photos/92148291?w=1920&h=1080",
                _apiUris.Photo("92148291", 1920, 1080).ToString()
            );
        }

        [Fact]
        public void ShouldReturnPhotoDownloadLinkById() {
            Assert.Equal(
                "https://api.unsplash.com/photos/92148291/download/",
                _apiUris.PhotoDownloadLink("92148291").ToString()
            );
        }

        [Fact]
        public void ShouldReturnCollectionUriById() {
            Assert.Equal(
                "https://api.unsplash.com/collections/231822/",
                _apiUris.Collection("231822").ToString()
            );
        }

        [Fact]
        public void ShouldReturnCollectionPhotosUriById() {
            Assert.Equal(
                "https://api.unsplash.com/collections/231822/photos/",
                _apiUris.CollectionPhotos("231822").ToString()
            );
        }
    }
}
