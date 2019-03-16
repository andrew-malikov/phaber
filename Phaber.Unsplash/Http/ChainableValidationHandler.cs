using System;
using System.Net.Http;
using System.Linq;

namespace Phaber.Unsplash.Http {
    public sealed class ChainableValidationHandler : IValidatableHttpResponse {
        private readonly IValidatableHttpResponse _handler;
        private ChainableValidationHandler _chainedHandlers;

        public ChainableValidationHandler(
            IValidatableHttpResponse handler,
            params IValidatableHttpResponse[] handlers
        ) {
            _handler = handler;

            if (handlers.Length > 0)
                _chainedHandlers = new ChainableValidationHandler(
                    handlers.First(),
                    handlers.Take(handlers.Length - 1).ToArray()
                );
        }

        /// <summary>
        ///     Add <paramref name="handler"/>  to the chain
        /// </summary>
        /// <param name="handler">
        ///     Will be the next handler in the chain
        /// </param>
        /// <returns>
        ///     The next chain <paramref name="handler"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Throw if <paramref name="handler"/> is null
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Throw if chain already has next handler
        /// </exception>
        public ChainableValidationHandler Add(IValidatableHttpResponse handler) {
            if (handler == null)
                throw new ArgumentNullException();

            if (_chainedHandlers != null)
                throw new InvalidOperationException();

            _chainedHandlers = new ChainableValidationHandler(handler);

            return _chainedHandlers;
        }

        public void Handle(HttpResponseMessage response) {
            _handler.Handle(response);

            if (_chainedHandlers != null)
                _chainedHandlers.Handle(response);
        }
    }
}