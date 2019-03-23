using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phaber.Unsplash.Test {
    [TestClass]
    public class ApiUrisTests {
        private ApiUris _apiUris;

        public ApiUrisTests() {
            _apiUris = new ApiUris(
                new Uri("https://api.unsplash.com/")
            );
        }

        [TestMethod]
        public void ShouldReturnPhotoUriById() {
            Assert.AreEqual(
                "https://api.unsplash.com/photos/92148291/",
                _apiUris.Photo("92148291").ToString()
            );
        }

        [TestMethod]
        public void ShouldReturnPhotoUriByIdWithResolution() {
            Assert.AreEqual(
                "https://api.unsplash.com/photos/92148291?w=1920&h=1080",
                _apiUris.Photo("92148291", 1920, 1080).ToString()
            );
        }

        [TestMethod]
        public void ShouldReturnPhotoDownloadLinkById() {
            Assert.AreEqual(
                "https://api.unsplash.com/photos/92148291/download/",
                _apiUris.PhotoDownloadLink("92148291").ToString()
            );
        }

        [TestMethod]
        public void ShouldReturnCollectionUriById() {
            Assert.AreEqual(
                "https://api.unsplash.com/collections/231822/",
                _apiUris.Collection("231822").ToString()
            );
        }

        [TestMethod]
        public void ShouldReturnCollectionPhotosUriById() {
            Assert.AreEqual(
                "https://api.unsplash.com/collections/231822/photos/",
                _apiUris.CollectionPhotos("231822").ToString()
            );
        }
    }
}
