using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PingDong.Azure.Function;
using PingDong.Http;
using PingDong.Newmoon.Venues.Http;
using PingDong.Newmoon.Venues.Settings;
using Venues.Function.Extensions;

[assembly: FunctionsStartup(typeof(PingDong.Newmoon.Venues.Startup))]
namespace PingDong.Newmoon.Venues
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Build configuration
            builder.Services.AddConfiguration<AppSettings>();
            
            // Validation
            builder.Services.AddScoped<IValidatorFactory, ValidatorFactory>();

            // Logging
            builder.Services.AddLogging(); 
            builder.Services.AddApplicationInsightsTelemetry();

            // Http
            builder.Services.AddTransient<IHttpRequestHelper, HttpRequestHelper>();

            // Core
            var core = new Core.Registrar();
            core.Register(builder.Services);

            // Infrastructure
            //var infrastructure = new Infrastructure.Registrar();
            //infrastructure.Register(builder.Services, config);
            
            // Services
            var services = new Services.Registrar();
            services.Register(builder.Services);
        }
    }
}
