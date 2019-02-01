using System;
using System.Runtime.Serialization;

namespace Phaber.Unsplash.Exceptions {
    [Serializable]
    public class RateLimitExceededException : Exception {
        public readonly RateLimit RateLimit;
        public RateLimitExceededException(RateLimit rateLimit) {
            RateLimit = rateLimit;
        }

        public RateLimitExceededException(RateLimit rateLimit, string message) : base(message) {
            RateLimit = rateLimit;
        }

        public RateLimitExceededException(
            RateLimit rateLimit, string message, Exception inner
        ) : base(message, inner) {
            RateLimit = rateLimit;
        }

        protected RateLimitExceededException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context) { }
    }
}