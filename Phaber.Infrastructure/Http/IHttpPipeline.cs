using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Phaber.Infrastructure.Models;
using Phaber.Unsplash;
using Phaber.Unsplash.Errors;

namespace Phaber.Infrastructure.Http {
    public interface IHttpPipeline {
        HttpRequestMessage ApplyCredentials(
            HttpRequestMessage request,
            Credentials credentials
        );

        IEnumerable<IError> ResolveErrors(HttpResponseMessage response);

        HttpRequestMessage ApplyBody(HttpRequestMessage request, object body);

        Task<FallibleBodyResponse<TB>> ExtractBody<TB>(
            HttpResponseMessage response
        ) where TB : class;

        Task<string> ExtractBodyAsPlain(HttpResponseMessage response);
    }
}