using System.IO;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PingDong.CleanArchitect.Service;
using PingDong.Newmoon.Places.Functions;

[assembly: FunctionsStartup(typeof(PingDong.Newmoon.Places.Startup))]
namespace PingDong.Newmoon.Places
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Configuration
            var config = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                                 .AddEnvironmentVariables()
                                 .Build();
            builder.Services.AddSingleton<IConfiguration>(config);
            // Multiple tenant
            builder.Services.AddTransient<ITenantProvider<string>, TenantProvider>();

            // Core
            var core = new Core.Registrar();
            core.Register(builder.Services);

            // Infrastructure
            var infrastructure = new Infrastructure.Registrar();
            infrastructure.Register(builder.Services, config);

            // Services
            var services = new Service.Registrar();
            services.Register(builder.Services);
        }
    }
}
