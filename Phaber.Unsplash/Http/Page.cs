using System;
using System.Collections.Generic;

namespace Phaber.Unsplash.Http {
    public class Page {
        public readonly int Number;
        public readonly int PerPage = 0;
        public readonly int Pages = 0;

        public Uri LinkToNext { get; private set; }
        public bool HasLinkToNext => LinkToNext != null;

        public Uri LinkToPrevious { get; private set; }
        public bool HasLinkToPrevious => LinkToPrevious != null;

        public Uri LinkToFirst { get; private set; }
        public bool HasLinkToFirst => LinkToFirst != null;

        public Uri LinkToLast { get; private set; }
        public bool HasLinkToLast => LinkToLast != null;

        public readonly Uri Link;

        public Page(Dictionary<string, string> headers, Uri link, int pageNumber = 1) {
            Link = link;
            Number = pageNumber;

            if (headers.ContainsKey("X-Per-Page"))
                PerPage = int.Parse(headers["X-Per-Page"]);

            if (headers.ContainsKey("X-Total") && PerPage > 0)
                Pages = int.Parse(headers["X-Total"]) / PerPage;

            if (headers.ContainsKey("Link"))
                SetupLinks(new ParsedLinks(headers["Link"]).Values);
        }

        private void SetupLinks(Dictionary<string, Uri> links) {
            if (links.ContainsKey("next"))
                LinkToNext = links["next"];

            if (links.ContainsKey("prev"))
                LinkToPrevious = links["prev"];

            if (links.ContainsKey("last"))
                LinkToLast = links["last"];

            if (links.ContainsKey("first"))
                LinkToFirst = links["first"];
        }

        protected Page(
            Uri link,
            int pageNumber,
            int perPage,
            int pages,
            PaginationDirection direction
        ) {
            Number = pageNumber;
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

        public static Page From(InitialPage initialPage) {
            return new Page(
                initialPage.Link,
                initialPage.PageNumber,
                initialPage.PerPage,
                1,
                initialPage.Direction
            );
        }
    }
}