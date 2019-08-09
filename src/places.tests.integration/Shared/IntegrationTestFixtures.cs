using Xunit;

namespace PingDong.Newmoon.Places.IntegrationTests
{
    [CollectionDefinition(nameof(IntegrationTestFixtures))]
    public class IntegrationTestFixtures : ICollectionFixture<IntegrationTestFixture>
    {
    }
}
