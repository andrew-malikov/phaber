using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Optional;
using Optional.Unsafe;
using Phaber.Infrastructure.Errors;
using Phaber.Infrastructure.Http;
using Phaber.Infrastructure.Models;
using Phaber.Unsplash.Entities;
using Phaber.Unsplash.Models;

namespace Phaber.Unsplash.Clients {
    public class PhotoClient : IPhotoClient {
        private readonly ApiUris _endpoints;
        private readonly IConnection _connection;

        public PhotoClient(ApiUris endpoints, IConnection connection) {
            _endpoints = endpoints;
            _connection = connection;
        }

        public Task<IFallibleBodyResponse<Photo>> GetPhotoAsync(Photo photo) {
            return GetPhotoAsync(photo.Id);
        }

        public Task<IFallibleBodyResponse<Photo>> GetPhotoAsync(Photo photo, int width, int height) {
            return GetPhotoAsync(photo.Id, width, height);
        }

        public async Task<IFallibleBodyResponse<Photo>> GetPhotoAsync(string id) {
            return await _connection.MakeRequest<Photo>(
                _endpoints.Photo(id),
                HttpMethod.Get
            );
        }

        public async Task<IFallibleBodyResponse<Photo>> GetPhotoAsync(string id, int width, int height) {
            return await _connection.MakeRequest<Photo>(
                _endpoints.Photo(id, width, height),
                HttpMethod.Get
            );
        }

        public async Task<IFallibleBodyResponse<Uri>> GetDownloadLinkAsync(string id) {
            return await _connection.MakeRequest(
                _endpoints.PhotoDownloadLink(id),
                HttpMethod.Get,
                body => new Uri(
                    JObject.Parse(body)["url"].ToString()
                )
            );
        }

        public async Task<IFallibleBodyResponse<Stream>> FetchPhotoContentStreamAsync(string id) {
            var downloadLink = await GetDownloadLinkAsync(id);

            if (!downloadLink.IsSuccess) {
                return downloadLink.Convert(
                    Option.None<Stream>(),
                    new Error(
                        $"photo id: {id}",
                        "can't load the download link for photo"
                    )
                );
            }

            return await FetchPhotoContentStreamAsync(
                downloadLink.Retrieve().ValueOrFailure()
            );
        }

        public async Task<IFallibleBodyResponse<Stream>> FetchPhotoContentStreamAsync(Uri photoUri) {
            var response = await _connection.MakeStreamRequest(
                new Uri(photoUri.PathAndQuery),
                HttpMethod.Get
            );

            if (!response.IsSuccess) {
                return HttpResponse<Stream>.OfFailure();
            }

            return HttpResponse<Stream>.OfSuccessful(
                await response.Retrieve().ValueOrFailure().Content.ReadAsStreamAsync()
            );
        }
    }
}