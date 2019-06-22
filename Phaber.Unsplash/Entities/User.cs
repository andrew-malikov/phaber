using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Phaber.Unsplash.Entities {
    /// <summary>
    /// Represents an user class from Unsplash API.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class User {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string TwitterUsername { get; set; }

        public string PortfolioUrl { get; set; }

        public string Bio { get; set; }

        public string Location { get; set; }

        /// <summary>
        /// User's liked photos count
        /// </summary>
        public int TotalLikes { get; set; }

        public int TotalPhotos { get; set; }

        public int TotalCollections { get; set; }

        public string UpdatedAt { get; set; }

        /// <summary>
        /// True if the current authenticated user follows this user
        /// </summary>
        public bool FollowedByUser { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingCount { get; set; }

        public int Downloads { get; set; }

        public ProfileImage ProfileImage { get; set; }

        public Badge Badge { get; set; }

        public UserLinks Links { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ProfileImage {
        public string Small { get; set; }

        public string Medium { get; set; }

        public string Large { get; set; }
    }

    /// <summary>
    /// Badge (a user specific role).
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Badge {
        public string Title { get; set; }

        /// <summary>
        /// True if it's the primary badge.
        /// </summary>
        public bool Primary { get; set; }

        /// <summary>
        /// Badge's description.
        /// </summary>
        public string Slug { get; set; }

        public string Link { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class UserLinks {
        public string Self { get; set; }

        public string Html { get; set; }

        public string Photos { get; set; }

        public string Likes { get; set; }

        public string Portfolio { get; set; }
    }
}
