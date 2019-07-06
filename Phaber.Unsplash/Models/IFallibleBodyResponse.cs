using Optional;

namespace Phaber.Unsplash.Models {
    public interface IFallibleBodyResponse<TV> : IFallibleResponse {
        Option<TV> Retrieve();
    }
}