using Phaber.Unsplash;

namespace Phaber.Infrastructure.Tests.Environment {
    public class EnvironmentCredentials : Credentials {
        public EnvironmentCredentials() : this(
            "UNSPLASH_APP_ID",
            "UNSPLASH_SECRET"
        ) { }

        public EnvironmentCredentials(
            string appIdKey,
            string secretKey
        ) : base(
            System.Environment.GetEnvironmentVariable(appIdKey),
            System.Environment.GetEnvironmentVariable(secretKey)
        ) { }
    }
}