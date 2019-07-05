using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Phaber.Unsplash.Entities {
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Photo {
        public string Id { get; set; }

        public string Description { get; set; }

        public string CreatedAt { get; set; }

        public string UpdatedAt { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Color { get; set; }

        public int Downloads { get; set; }

        public int Likes { get; set; }

        /// <summary>
        /// Whether the photo has been liked by the current user if a user is logged.
        /// </summary>
        [JsonProperty("liked_by_user")]
        public bool IsLikedByUser { get; set; }

        /// <summary>
        /// The photo's collection where the photo is included, if any.
        /// </summary>
        public List<Collection> CurrentUserCollection { get; set; }

        /// <summary>
        /// Absolute photo's URLs (for different photo's sizes).
        /// </summary>
        public Urls Urls { get; set; }

        public List<Category> Categories { get; set; }

        public User User { get; set; }

        /// <summary>
        /// Camera specifications.
        /// </summary>
        public Exif Exif { get; set; }

        public Location Location { get; set; }

        /// <summary>
        /// Photo's link relations
        /// </summary>
        public PhotoLinks Links { get; set; }

        protected bool Equals(Photo other) {
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (other.GetType() != GetType()) return false;

            return Equals((Photo) other);
        }

        public override int GetHashCode() {
            return Id != null ? Id.GetHashCode() : 0;
        }
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class PhotoLinks {
        public string Self { get; set; }

        public string Html { get; set; }

        /// <summary>
        /// Download location of this photo.
        /// </summary>
        public string Download { get; set; }

        /// <summary>
        ///  API Download location of this photo (check if direct download).
        /// </summary>
        public string DownloadLocation { get; set; }
    }

    /// <summary>
    /// Camera specifications.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Exif {
        /// <summary>
        /// Camera’s brand.
        /// </summary>
        public string Make { get; set; }

        public string Model { get; set; }

        public string ExposureTime { get; set; }

        public string Aperture { get; set; }

        public string FocalLength { get; set; }

        public int? Iso { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Location {
        /// <summary>
        /// Full location's name (district + city + country (if available))
        /// </summary>
        public string Title { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        /// <summary>
        /// Location’s position (latitude, longitude).
        /// </summary>
        public Position Position { get; set; }
    }

    /// <summary>
    /// Represents a geographical position (latitude, longitude).
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Position {
        public int Latitude { get; set; }

        public int Longitude { get; set; }
    }

    /// <summary>
    /// Photo's URLs linking to the direct photo (full screen).
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Urls {
        /// <summary>
        /// URL linking to the photo in native resolution and uncompressed.
        /// </summary>
        public string Raw { get; set; }

        public string Full { get; set; }

        public string Regular { get; set; }

        public string Small { get; set; }

        [JsonProperty("thumb")] public string Thumbnail { get; set; }

        /// <summary>
        /// URL linking to the photo in a custom size if specified by the user.
        /// </summary>
        public string Custom { get; set; }
    }

    /// <summary>
    /// Photo's category.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Category {
        public string Id { get; set; }

        public string Title { get; set; }

        public int PhotoCount { get; set; }

        public CategoryLinks Links { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class CategoryLinks {
        /// <summary>
        /// Link of this category.
        /// </summary>
        public string Self { get; set; }

        /// <summary>
        /// Link to all photos in this category.
        /// </summary>
        public string Photos { get; set; }
    }
}