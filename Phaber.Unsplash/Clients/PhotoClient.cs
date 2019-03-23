using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Phaber.Unsplash.Http;
using Phaber.Unsplash.Models;

namespace Phaber.Unsplash.Clients {
    public class PhotoClient {
        private readonly ApiUris _endpoints;
        private readonly Connection _connection;

        public PhotoClient(ApiUris endpoints, Connection connection) {
            _endpoints = endpoints;
            _connection = connection;
        }

        public Task<Photo> GetPhotoAsync(Photo photo) {
            return GetPhotoAsync(photo.Id);
        }

        public Task<Photo> GetPhotoAsync(Photo photo, int width, int height) {
            return GetPhotoAsync(photo.Id, width, height);
        }

        public async Task<Photo> GetPhotoAsync(string id) {
            return (await _connection.MakeRequest<Photo>(
                _endpoints.Domain,
                _endpoints.Photo(id),
                HttpMethod.Get,
                async body => JsonConvert.DeserializeObject<Photo>(
                    await body.AsStringAsync()
                )
            )).Body;
        }

        public async Task<Photo> GetPhotoAsync(string id, int width, int height) {
            return (await _connection.MakeRequest<Photo>(
                _endpoints.Domain,
                _endpoints.Photo(id, width, height),
                HttpMethod.Get,
                async body => JsonConvert.DeserializeObject<Photo>(
                    await body.AsStringAsync()
                )
            )).Body;
        }

        public async Task<Uri> GetDownloadLinkAsync(string id) {
            return (await _connection.MakeRequest<Uri>(
                _endpoints.Domain,
                _endpoints.PhotoDownloadLink(id),
                HttpMethod.Get,
                async body => new Uri(
                    JObject.Parse(await body.AsStringAsync())["url"].ToString()
                )
            )).Body;
        }

        public async Task<Stream> FetchPhotoContentStreamAsync(string id) {
            return await FetchPhotoContentStreamAsync(
                await GetDownloadLinkAsync(id)
            );
        }

        public async Task<Stream> FetchPhotoContentStreamAsync(Uri photoUri) {
            return await (await _connection.MakeStreamRequest(
                _endpoints.Domain,
                new Uri(photoUri.PathAndQuery),
                HttpMethod.Get)
            ).Content.ReadAsStreamAsync();
        }
    }
}