using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Phaber.Unsplash.Entities {
    /// <summary>
    /// Represents a collection of photos.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Collection {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string PublishedAt { get; set; }

        public string UpdatedAt { get; set; }

        public bool IsCurated { get; set; }

        public bool IsFeatured { get; set; }

        public int TotalPhotos { get; set; }

        public bool IsPrivate { get; set; }

        public string ShareKey { get; set; }

        public Photo CoverPhoto { get; set; }

        public User User { get; set; }

        public CollectionLinks Links { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class CollectionLinks {
        public string Self { get; set; }

        public string Html { get; set; }

        public string Photos { get; set; }

        public string Related { get; set; }
    }
}
