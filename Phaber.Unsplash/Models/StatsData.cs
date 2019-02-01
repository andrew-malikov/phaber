using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Phaber.Unsplash.Models {
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class StatsData {
        /// <summary>
        /// Total count of a specific stat.
        /// </summary>
        public int Total { get; set; }

        public StatsHistorical Historical { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class StatsHistorical {
        /// <summary>
        /// Total number of stat for the past 'quantity' 'resolution' (eg. 30 days).
        /// </summary>
        public double Change { get; set; }

        /// <summary>
        /// Average number of stat for the past 'quantity' 'resolution' (eg. 30 days).
        /// </summary>
        public int Average { get; set; }

        /// <summary>
        /// The frequency of the stats.
        /// </summary>
        public string Resolution { get; set; }

        /// <summary>
        /// The amount of for each stat.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// List of data sets.
        /// </summary>
        public List<StatsValue> Values { get; set; }
    }

    /// <summary>
    /// A data set.
    /// </summary>
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class StatsValue {
        public string Date { get; set; }

        public double Value { get; set; }
    }
}
