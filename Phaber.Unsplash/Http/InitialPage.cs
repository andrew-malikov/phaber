using System;

namespace Phaber.Unsplash.Http {
    public class InitialPage {
        public readonly Uri Link;
        public readonly int PageNumber;
        public readonly int PerPage;
        public readonly PaginationDirection Direction;

        public InitialPage(
            Uri link,
            int pageNumber,
            int perPage,
            PaginationDirection direction = PaginationDirection.Ascending
        ) {
            Link = link;
            PageNumber = pageNumber;
            PerPage = perPage;
            Direction = direction;
        }
    }

    public enum PaginationDirection {
        Ascending,
        Descending
    }
}

