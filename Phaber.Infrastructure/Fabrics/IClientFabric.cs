using Phaber.Unsplash.Clients;

namespace Phaber.Infrastructure.Fabrics {
    public interface IClientFabric {
        IPhotoClient GetPhotoClient();
        ICollectionClient GetCollectionClient();
    }
}