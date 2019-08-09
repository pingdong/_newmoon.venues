using System;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using PingDong.Azure.Functions.Testing;

namespace PingDong.Newmoon.Places.IntegrationTests
{
    public class IntegrationTestFixture : FunctionsHost
    {
        public IntegrationTestFixture()
        {
            // Configuration
            var configuration = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                                        .AddEnvironmentVariables()
                                        .Build();

            var dotnetExePath = Environment.ExpandEnvironmentVariables(configuration["host:dotnetExePath"]);
            var funcHostExePath = Environment.ExpandEnvironmentVariables(configuration["host:funcHostExePath"]);
            var funcAppExePath = configuration["host:funcAppExePath"];
            var port = Convert.ToInt32(configuration["host:port"]);

            Initialize(dotnetExePath, funcHostExePath, funcAppExePath, port);

            Thread.Sleep(2000);
        }
    }
}
