using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Phaber.Unsplash.Exceptions;
using Phaber.Unsplash.Helpers;

namespace Phaber.Unsplash.Http {
    public class PagedResponse<T> : IEnumerator<Response<T>>, IEnumerable<Response<T>> {
        private Page _currentPage;
        public int PageNumber => _currentPage.Number;
        public int Pages => _currentPage.Pages;
        public int PerPage => _currentPage.PerPage;

        private Response<T> _dataPage;
        public Response<T> Current =>
            _dataPage != null
            ? _dataPage
            : throw new InvalidOperationException();

        object IEnumerator.Current => Current;

        private Func<Uri, Task<Response<T>>> _requestHandler;

        public PagedResponse(
            Page page,
            Func<Uri, Task<Response<T>>> requestHandler
        ) {
            _currentPage = page;
            _requestHandler = requestHandler;
        }

        protected async Task<Response<T>> MoveTo(Uri pageLink) {
            if (pageLink == null)
                throw new PageNotExistException();

            var response = await _requestHandler(pageLink);

            _currentPage = new Page(
                response.Headers,
                _currentPage.LinkToNext,
                _currentPage.Number + 1
            );

            return response;
        }

        public bool MoveNext() {
            if (!_currentPage.HasLinkToNext)
                return false;

            _dataPage = MoveTo(_currentPage.LinkToNext).Result;
            return true;
        }

        public void Reset() {
            throw new NotImplementedException();
        }


        public IEnumerator<Response<T>> GetEnumerator() {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this;
        }

        public void Dispose() { }
    }
}