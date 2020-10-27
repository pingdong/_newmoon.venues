using Microsoft.Extensions.Configuration;
using PingDong.Testings.TestFixtures;
using System.IO;

namespace PingDong.Newmoon.Venues.IntegrationTests
{
    public class HttpTestFixture : HttpTestFixtureBase
    {
        public HttpTestFixture()
        {
            var cfgBuilder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory());

            cfgBuilder.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();

            var configuration = cfgBuilder.Build();

            Initialize(configuration["FuncApp:BaseUrl"]);
        }
    }
}
