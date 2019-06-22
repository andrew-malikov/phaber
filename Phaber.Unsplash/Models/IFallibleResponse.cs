using System.Collections.Generic;
using Phaber.Infrastructure.Errors;

namespace Phaber.Unsplash.Models {
    public interface IFallibleResponse {
        IEnumerable<Error> Errors { get; }
        bool IsSuccess { get; }
    }
}