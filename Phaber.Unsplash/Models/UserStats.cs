using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Phaber.Unsplash.Models {
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class UserStats {
        public string Username { get; set; }

        public StatsData Downloads { get; set; }

        public StatsData Views { get; set; }

        public StatsData Likes { get; set; }
    }
}
