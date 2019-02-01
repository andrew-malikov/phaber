using System.Net.Http;

namespace Phaber.Unsplash.Http {
    public interface IValidatableHttpResponse {
        void Handle(HttpResponseMessage response);
    }
}