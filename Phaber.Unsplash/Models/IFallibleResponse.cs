using System.Collections.Generic;
using Phaber.Unsplash.Errors;

namespace Phaber.Unsplash.Models {
    public interface IFallibleResponse {
        IEnumerable<IError> Errors { get; }
        bool IsSuccess { get; }
    }
}