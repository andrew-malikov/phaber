using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Optional;
using Phaber.Infrastructure.Errors;
using Phaber.Infrastructure.Http;
using Phaber.Unsplash.Models;

namespace Phaber.Infrastructure.Models {
    public class PageableHttpResponse<TV> : IPageableResponse<TV> where TV : class {
        private IPageable _pageable;
        private IFalliblePageResponse<TV> _page;
        private readonly Func<Uri, Task<HttpResponse<TV>>> _resolvableRequest;

        public IFalliblePageResponse<TV> Current =>
            _page ?? throw new InvalidOperationException();

        object IEnumerator.Current => Current;

        public PageableHttpResponse(
            IPageable pageable,
            Func<Uri, Task<HttpResponse<TV>>> resolvableRequest
        ) {
            _pageable = pageable;
            _resolvableRequest = resolvableRequest;
        }

        protected async Task<IFalliblePageResponse<TV>> MoveTo(Uri pageLink) {
            var response = await _resolvableRequest(pageLink);

            _pageable = new Pageable(
                response.Headers,
                _pageable.LinkToNext,
                _pageable.Page + 1
            );

            if (!response.IsSuccess) {
                return HttpPageResponse<TV>.OfFailure(
                    response,
                    Option.Some(_pageable),
                    new Error("", "the failure in pageable response")
                );
            }

            return HttpPageResponse<TV>.OfSuccessful(response, _pageable);
        }

        public bool MoveNext() {
            if (!_pageable.HasLinkToNext) {
                return false;
            }

            _page = MoveTo(_pageable.LinkToNext).Result;

            return true;
        }

        public void Reset() {
            throw new NotImplementedException();
        }

        public void Dispose() { }

        public IEnumerator<IFalliblePageResponse<TV>> GetEnumerator() {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}