using System.Collections.Generic;
using System.Threading.Tasks;
using Phaber.Unsplash.Entities;
using Phaber.Unsplash.Models;

namespace Phaber.Unsplash.Clients {
    public interface ICollectionClient {
        Task<IFallibleBodyResponse<Collection>> GetAsync(Collection collection);
        Task<IFallibleBodyResponse<Collection>> GetAsync(string collectionId);

        IPageableResponse<IEnumerable<Photo>> GetPhotos(
            string collectionId,
            int page = 1,
            int perPage = 30
        );
    }
}