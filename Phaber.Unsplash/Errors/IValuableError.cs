using Optional;

namespace Phaber.Unsplash.Errors {
    public interface IValuableError<T> {
        Option<T> Valuable { get; }
    }
}