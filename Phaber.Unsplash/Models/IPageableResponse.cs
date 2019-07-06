using System.Collections.Generic;

namespace Phaber.Unsplash.Models {
    public interface IPageableResponse<TV> :
        IEnumerator<IFalliblePageResponse<TV>>,
        IEnumerable<IFalliblePageResponse<TV>> { }
}