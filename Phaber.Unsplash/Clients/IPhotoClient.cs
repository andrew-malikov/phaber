using System;
using System.IO;
using System.Threading.Tasks;
using Phaber.Unsplash.Models;

namespace Phaber.Unsplash.Clients {
    public interface IPhotoClient {
        Task<Photo> GetPhotoAsync(Photo photo);
        Task<Photo> GetPhotoAsync(Photo photo, int width, int height);
        Task<Photo> GetPhotoAsync(string id);
        Task<Photo> GetPhotoAsync(string id, int width, int height);
        Task<Uri> GetDownloadLinkAsync(string id);
        Task<Stream> FetchPhotoContentStreamAsync(string id);
        Task<Stream> FetchPhotoContentStreamAsync(Uri photoUri);
    }
}