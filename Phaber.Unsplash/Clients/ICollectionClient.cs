using System.Collections.Generic;
using System.Threading.Tasks;
using Phaber.Unsplash.Entities;
using Phaber.Unsplash.Http;

namespace Phaber.Unsplash.Clients {
    public interface ICollectionClient {
        Task<Collection> GetAsync(Collection collection);
        Task<Collection> GetAsync(string collectionId);
        PagedResponse<List<Photo>> GetPhotos(
            string collectionId,
            int page = 1,
            int perPage = 30
        );
    }
}
