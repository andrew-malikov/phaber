using System;
using System.Collections.Generic;
using Phaber.Infrastructure.Http;
using Phaber.Unsplash.Models;

namespace Phaber.Infrastructure.Models {
    public class Pageable : IPageable {
        public int Page { get; private set; }
        public int Pages { get; private set; }
        public int PerPage { get; private set; }

        public Uri LinkToNext { get; private set; }
        public bool HasLinkToNext => LinkToNext != null;

        public Uri LinkToPrevious { get; private set; }
        public bool HasLinkToPrevious => LinkToPrevious != null;

        public Uri LinkToFirst { get; private set; }
        public bool HasLinkToFirst => LinkToFirst != null;

        public Uri LinkToLast { get; private set; }
        public bool HasLinkToLast => LinkToLast != null;

        public readonly Uri Link;

        public Pageable(
            IReadOnlyDictionary<string, string> headers,
            Uri link,
            int page = 1
        ) {
            Link = link;
            Page = page;

            if (headers.ContainsKey("X-Per-Page")) {
                PerPage = int.Parse(headers["X-Per-Page"]);
            }

            if (headers.ContainsKey("X-Total") && PerPage > 0) {
                Pages = int.Parse(headers["X-Total"]) / PerPage;
            }

            if (headers.ContainsKey("Link")) {
                SetupLinks(new ParsedLinks(headers["Link"]).Values);
            }
        }

        private void SetupLinks(IReadOnlyDictionary<string, Uri> links) {
            if (links.ContainsKey("next")) {
                LinkToNext = links["next"];
            }

            if (links.ContainsKey("prev")) {
                LinkToPrevious = links["prev"];
            }

            if (links.ContainsKey("last")) {
                LinkToLast = links["last"];
            }

            if (links.ContainsKey("first")) {
                LinkToFirst = links["first"];
            }
        }

        protected Pageable(
            Uri link,
            int page,
            int perPage,
            int pages,
            PaginationDirection direction
        ) {
            Page = page;
            PerPage = perPage;
            Pages = pages;

            switch (direction) {
                case PaginationDirection.Ascending:
                    LinkToNext = link;
                    break;
                case PaginationDirection.Descending:
                    LinkToPrevious = link;
                    break;
            }
        }

        public static Pageable From(InitialPage initialPage) {
            return new Pageable(
                initialPage.Link,
                initialPage.Page,
                initialPage.PerPage,
                1,
                initialPage.Direction
            );
        }
    }
}