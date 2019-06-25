using Optional;
using Phaber.Unsplash.Errors;

namespace Phaber.Infrastructure.Errors {
    public class ValuableError<T> : IError, IValuableError<T> {
        public string Message { get; }
        public Option<T> Valuable { get; }

        public ValuableError(string message, T valuable) {
            Message = message;
            Valuable = valuable != null ? Option.Some(valuable) : Option.None<T>();
        }
    }
}