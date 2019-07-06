using System.Collections.Generic;
using System.Linq;
using Optional;
using Phaber.Unsplash.Errors;

namespace Phaber.Infrastructure.Errors {
    public class ChainedErrorResolver<T> : IErrorResolver<T> {
        private readonly IErrorResolver<T> _resolver;
        private readonly Option<IErrorResolver<T>> _chain;

        public ChainedErrorResolver(
            IErrorResolver<T> resolver,
            params IErrorResolver<T>[] resolvers
        ) {
            _resolver = resolver;

            if (resolvers.Length > 0) {
                _chain = Option.Some<IErrorResolver<T>>(
                    new ChainedErrorResolver<T>(
                        resolvers.First(),
                        resolvers.Skip(1).ToArray()
                    ));
            }
            else {
                _chain = Option.None<IErrorResolver<T>>();
            }
        }

        public IEnumerable<IError> Resolve(T resolvable) {
            var errors = _resolver.Resolve(resolvable).ToList();

            _chain.MatchSome(nestedResolver => {
                errors.AddRange(nestedResolver.Resolve(resolvable));
            });

            return errors;
        }
    }
}