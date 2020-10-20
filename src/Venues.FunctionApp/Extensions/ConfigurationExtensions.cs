using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PingDong.Newmoon.Venues;
using System;
using System.IO;

namespace Venues.Function.Extensions
{
    internal static class ConfigurationExtensions
    {
        public static T AddConfiguration<T>(this IServiceCollection services)
            where T : class, new()
        {
            var cfgBuilder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory());

            // Shared configuration
            var appConfigurationConnection = Environment.GetEnvironmentVariable("ConnectionString:AzureConfiguration");
            if (!string.IsNullOrWhiteSpace(appConfigurationConnection))
                cfgBuilder.AddAzureAppConfiguration(Environment.GetEnvironmentVariable("ConnectionString:AzureConfiguration"), optional: true);

            // Local Development
            cfgBuilder.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                      .AddUserSecrets<Startup>(optional: true);

            // Environment-bound
            cfgBuilder.AddEnvironmentVariables();
            
            var config = cfgBuilder.Build();
            services.AddSingleton<IConfiguration>(config);

            T appSettings = null;
            services.AddOptions<T>()
                    .Configure<IConfiguration>((settings, configuration) =>
                        {
                            configuration.Bind(settings);
                            appSettings = settings;
                        })
                    .ValidateDataAnnotations();

            return appSettings;
        }
    }
}
