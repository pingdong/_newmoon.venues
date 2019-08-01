using PingDong.Azure.Functions.Testing;
using Xunit;

namespace PingDong.Newmoon.Places.IntegrationTests
{
    [CollectionDefinition("Integration Test")]
    public class CollectionFixture : ICollectionFixture<FunctionHost>
    {
    }
}
