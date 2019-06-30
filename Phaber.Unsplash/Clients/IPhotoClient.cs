using System;
using System.IO;
using System.Threading.Tasks;
using Phaber.Unsplash.Entities;
using Phaber.Unsplash.Models;

namespace Phaber.Unsplash.Clients {
    public interface IPhotoClient {
        Task<IFallibleBodyResponse<Photo>> GetPhotoAsync(Photo photo);
        Task<IFallibleBodyResponse<Photo>> GetPhotoAsync(Photo photo, int width, int height);
        Task<IFallibleBodyResponse<Photo>> GetPhotoAsync(string id);
        Task<IFallibleBodyResponse<Photo>> GetPhotoAsync(string id, int width, int height);
        Task<IFallibleBodyResponse<Uri>> GetDownloadLinkAsync(string id);
        Task<IFallibleBodyResponse<Stream>> FetchPhotoContentStreamAsync(string id);
        Task<IFallibleBodyResponse<Stream>> FetchPhotoContentStreamAsync(Uri photoUri);
    }
}