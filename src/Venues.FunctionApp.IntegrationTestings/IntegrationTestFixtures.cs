using PingDong.Azure.FunctionApp.Testings;
using Xunit;

namespace PingDong.Newmoon.Venues.IntegrationTestings
{
    [CollectionDefinition(nameof(IntegrationTestFixtures))]
    public class IntegrationTestFixtures
        : ICollectionFixture<FunctionAppTestFixture>
        , ICollectionFixture<HttpTestFixture>
        , ICollectionFixture<JsonTestFixture>
    {
    }
}
