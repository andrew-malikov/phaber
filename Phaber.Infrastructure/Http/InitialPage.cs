using System;

namespace Phaber.Infrastructure.Http {
    public class InitialPage {
        public readonly Uri Link;
        public int Page { get; private set; }
        public int PerPage { get; private set; }
        public readonly PaginationDirection Direction;

        public InitialPage(
            Uri link,
            int page,
            int perPage,
            PaginationDirection direction = PaginationDirection.Ascending
        ) {
            Link = link;
            Page = page;
            PerPage = perPage;
            Direction = direction;
        }
    }

    public enum PaginationDirection {
        Ascending,
        Descending
    }
}