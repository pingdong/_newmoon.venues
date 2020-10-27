using PingDong.Azure.FunctionApp.Testings;
using Xunit;

namespace PingDong.Newmoon.Venues.IntegrationTests
{
    [CollectionDefinition(nameof(IntegrationTestFixtures))]
    public class IntegrationTestFixtures
        : ICollectionFixture<FunctionAppTestFixture>
        , ICollectionFixture<HttpTestFixture>
        , ICollectionFixture<JsonTestFixture>
    {
    }
}
