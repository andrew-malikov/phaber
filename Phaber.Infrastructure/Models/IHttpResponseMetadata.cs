using System.Collections.Generic;
using System.Net;
using Phaber.Unsplash;

namespace Phaber.Infrastructure.Models {
    public interface IHttpResponseMetadata {
        HttpStatusCode StatusCode { get; }
        IReadOnlyDictionary<string, string> Headers { get; }

        RateLimit RateLimit { get; }
    }
}