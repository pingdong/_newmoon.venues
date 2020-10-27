using Microsoft.Extensions.Configuration;
using System.IO;
using PingDong.Newmoon.Venues.Settings;

namespace PingDong.Newmoon.Venues.Tests
{
    internal class SettingsHelper
    {
        internal static TestSettings Build()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\Venus.Function\local.settings.json"), true, true)
                .AddUserSecrets<AppSettings>(true, true)
                .AddEnvironmentVariables()
                .AddJsonFile("test.settings.json", true, true)
                .Build();

            var settings = new TestSettings();
            config.Bind(settings);

            return settings;
        }
    }
}
