using System;

namespace Phaber.Unsplash.Models {
    public interface IPageable {
        int Page { get; }
        int Pages { get; }
        int PerPage { get; }

        Uri LinkToNext { get; }
        bool HasLinkToNext { get; }
    }
}