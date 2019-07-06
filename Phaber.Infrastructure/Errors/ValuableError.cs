using Optional;
using Phaber.Unsplash.Errors;

namespace Phaber.Infrastructure.Errors {
    public class ValuableError<T> : Error, IValuableError<T> {
        public Option<T> Valuable { get; }

        public ValuableError(
            string debugMessage,
            string message,
            T valuable
        ) : base(debugMessage, message) {
            Valuable = valuable != null ? Option.Some(valuable) : Option.None<T>();
        }
    }
}