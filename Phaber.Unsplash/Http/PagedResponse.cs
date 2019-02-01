using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Phaber.Unsplash.Exceptions;
using Phaber.Unsplash.Helpers;

namespace Phaber.Unsplash.Http {
    public class PagedResponse<T> {
        public Page Page { get; private set; }
        private Func<Uri, Task<Response<T>>> _requestHandler;

        public PagedResponse(
            Page page,
            Func<Uri, Task<Response<T>>> requestHandler
        ) {
            Page = page;
            _requestHandler = requestHandler;
        }

        public async Task<Response<T>> Next() {
            return await MoveTo(new Uri(Page.LinkToNext.PathAndQuery));
        }

        protected async Task<Response<T>> MoveTo(Uri pageLink) {
            if (pageLink == null)
                throw new PageNotExistException();

            var response = await _requestHandler(
                new Uri(pageLink.PathAndQuery)
            );

            Page = new Page(response.Headers, Page.LinkToNext, Page.Number + 1);

            return response;
        }
    }
}