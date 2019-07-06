using System.Collections.Generic;

namespace Phaber.Unsplash.Errors {
    public interface IErrorResolver<in T> {
        IEnumerable<IError> Resolve(T resolvable);
    }
}