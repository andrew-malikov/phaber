using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Phaber.Unsplash.Entities {
    /// <summary>
    /// Unsplash statistics counts.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class UnsplashTotalStats {
        public double Photos { get; set; }

        public double Downloads { get; set; }

        public double Views { get; set; }

        public double Likes { get; set; }

        public double Photographers { get; set; }

        public double Pixels { get; set; }

        /// <summary>
        /// Average number of downloads per second for the past 7 days.
        /// </summary>
        public double DownloadsPerSecond { get; set; }

        /// <summary>
        /// Average number of views per second for the past 7 days.
        /// </summary>
        public double ViewsPerSecond { get; set; }

        public int Developers { get; set; }

        public int Applications { get; set; }

        public double Requests { get; set; }
    }

    /// <summary>
    /// Unsplash statistics counts for the past 30 days.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class UnsplashMonthlyStats {
        public double Downloads { get; set; }

        public double Views { get; set; }

        public double Likes { get; set; }

        public double NewPhotos { get; set; }

        public double NewPhotographers { get; set; }

        public double NewPixels { get; set; }

        public int NewDevelopers { get; set; }

        public int NewApplications { get; set; }

        public double NewRequests { get; set; }
    }
}
