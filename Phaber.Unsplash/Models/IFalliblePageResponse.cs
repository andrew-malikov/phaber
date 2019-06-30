using Optional;

namespace Phaber.Unsplash.Models {
    public interface IFalliblePageResponse<TV> : IFallibleBodyResponse<TV> {
        Option<IPageable> Pageable { get; }
    }
}