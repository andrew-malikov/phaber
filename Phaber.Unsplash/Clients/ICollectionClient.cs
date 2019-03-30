using System.Collections.Generic;
using System.Threading.Tasks;
using Phaber.Unsplash.Http;
using Phaber.Unsplash.Models;

namespace Phaber.Unsplash.Clients {
    public interface ICollectionClient {
        Task<Collection> GetAsync(Collection collection);
        Task<Collection> GetAsync(string collectionId);
        PagedResponse<List<Photo>> GetPhotosAsync(
            string collectionId,
            int page = 1,
            int perPage = 30
        );
    }
}