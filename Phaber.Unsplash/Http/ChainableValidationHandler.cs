using System.Net.Http;

namespace Phaber.Unsplash.Http {
    abstract public class ChainableValidationHandler : IValidatableHttpResponse {
        protected IValidatableHttpResponse _nextHandler;

        public ChainableValidationHandler(IValidatableHttpResponse handler = null) {
            _nextHandler = handler;
        }

        public abstract void Validate(HttpResponseMessage response);

        public void Handle(HttpResponseMessage response) {
            Validate(response);

            if (_nextHandler != null)
                _nextHandler.Handle(response);
        }
    }
}