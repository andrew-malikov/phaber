using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Phaber.Unsplash.Models;

namespace Phaber.Infrastructure.Http {
    public interface IConnection {
        Task<IFallibleBodyResponse<T>> MakeRequest<T>(
            Uri endpoint,
            HttpMethod method
        ) where T : class;

        Task<IFallibleBodyResponse<T>> MakeRequest<T>(
            Uri endpoint,
            Dictionary<string, string> headers,
            HttpMethod method
        ) where T : class;

        Task<IFallibleBodyResponse<T>> MakeRequest<T, TB>(
            Uri endpoint,
            TB body,
            HttpMethod method
        ) where T : class;

        Task<IFallibleBodyResponse<T>> MakeRequest<T, TB>(
            Uri endpoint,
            Dictionary<string, string> headers,
            TB body,
            HttpMethod method
        ) where T : class;
        
        Task<IFallibleBodyResponse<T>> MakeRequest<T>(
            Uri endpoint,
            HttpMethod method,
            Func<string, T> deserialize
        ) where T : class;

        Task<IFallibleBodyResponse<T>> MakeRequest<T>(
            Uri endpoint,
            Dictionary<string, string> headers,
            HttpMethod method,
            Func<string, T> deserialize
        ) where T : class;
        
        Task<IFallibleBodyResponse<T>> MakeRequest<T, TB>(
            Uri endpoint,
            TB body,
            HttpMethod method,
            Func<string, T> deserialize
        ) where T : class;

        Task<IFallibleBodyResponse<T>> MakeRequest<T, TB>(
            Uri endpoint,
            Dictionary<string, string> headers,
            TB body,
            HttpMethod method,
            Func<string, T> deserialize
        ) where T : class;
        
        Task<IFallibleResponse> MakePlainRequest(
            Uri endpoint,
            HttpMethod method
        );

        Task<IFallibleResponse> MakePlainRequest(
            Uri endpoint,
            Dictionary<string, string> headers,
            HttpMethod method
        );

        Task<IFallibleResponse> MakePlainRequest<TB>(
            Uri endpoint,
            TB body,
            HttpMethod method
        );

        Task<IFallibleResponse> MakePlainRequest<TB>(
            Uri endpoint,
            Dictionary<string, string> headers,
            TB body,
            HttpMethod method
        );

        Task<IFallibleBodyResponse<HttpResponseMessage>> MakeStreamRequest(
            Uri endpoint,
            HttpMethod method
        );

        Task<IFallibleBodyResponse<HttpResponseMessage>> MakeStreamRequest(
            Uri endpoint,
            Dictionary<string, string> headers,
            HttpMethod method
        );

        Task<IFallibleBodyResponse<HttpResponseMessage>> MakeStreamRequest<TB>(
            Uri endpoint,
            TB body,
            HttpMethod method
        );

        Task<IFallibleBodyResponse<HttpResponseMessage>> MakeStreamRequest<TB>(
            Uri endpoint,
            Dictionary<string, string> headers,
            TB body,
            HttpMethod method
        );

        IPageableResponse<TV> MakePagedRequest<TV>(
            Uri endpoint,
            int page,
            int perPage,
            HttpMethod method
        ) where TV : class;
    }
}