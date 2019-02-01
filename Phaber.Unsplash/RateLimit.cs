using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using Phaber.Unsplash.Helpers;

namespace Phaber.Unsplash {
    public class RateLimit {
        /// <summary>
        /// The maximum number of requests that the consumer is permitted to make per hour.
        /// </summary>
        public int Limit { get; private set; }

        /// <summary>
        /// The number of requests remaining in the current rate limit window.
        /// </summary>
        public int Remaining { get; private set; }

        /// <summary>
        /// The date and time at which the current rate limit window resets
        /// </summary>
        public long Reset { get; private set; }

        public bool IsLimitExceeded => Remaining == 0;

        public RateLimit(HttpResponseMessage response) : this(
            response
                .Headers
                .ToDictionary(h => h.Key, h => h.Value.Aggregate())
        ) { }

        public RateLimit(IDictionary<string, string> responseHeaders) : this(
            (int)GetHeaderValueAsInt32Safe(responseHeaders, "X-RateLimit-Limit"),
            (int)GetHeaderValueAsInt32Safe(responseHeaders, "X-RateLimit-Remaining")
        ) { }

        public RateLimit(int limit, int remaining) {
            Limit = limit;
            Remaining = remaining;

            var reset = DateTimeOffset.UtcNow;
            reset
                .AddHours(1)
                .Subtract(new TimeSpan(0, reset.Minute, reset.Second));

            Reset = reset.ToUnixTimeSeconds();
        }

        public RateLimit(int limit, int remaining, long reset) {
            Limit = limit;
            Remaining = remaining;
            Reset = reset;
        }

        private RateLimit() { }

        static long GetHeaderValueAsInt32Safe(IDictionary<string, string> responseHeaders, string key) {
            string value;
            long result;
            return !responseHeaders.TryGetValue(key, out value) || value == null || !long.TryParse(value, out result)
                ? 0
                : result;
        }

        public RateLimit Clone() => new RateLimit {
            Limit = Limit,
            Remaining = Remaining,
            Reset = Reset
        };
    }
}