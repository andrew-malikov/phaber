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

            if (headers.ContainsKey("Link")) {
                var links = new ParsedLinks(headers["Link"]).Values;

                LinkToNext = links["next"];
                LinkToPrevious = links["prev"];
                LinkToLast = links["last"];
                LinkToFirst = links["first"];
            }
        }

        protected Page(Uri link, int pageNumber, int perPage, int pages) {
            Link = link;
            Number = pageNumber;
            PerPage = perPage;
            Pages = pages;
        }

        public static Page Initial(Uri link, int pageNumber, int perPage, int pages = 0) {
            return new Page(link, pageNumber, perPage, pages);
        }
    }
}