using Phaber.Infrastructure.Fabrics;

namespace Phaber.Infrastructure.Tests.Environment {
    public class EnvironmentHttpClientFabric : HttpClientFabric {
        public EnvironmentHttpClientFabric() : base(
            new EnvironmentCredentials()
        ) { }
    }
}