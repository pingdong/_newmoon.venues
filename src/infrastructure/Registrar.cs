using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PingDong.CleanArchitect.Infrastructure;

namespace PingDong.Newmoon.Places.Infrastructure
{
    public class Registrar
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DefaultDbContext>(options => InitDbContextOption(config, options));

            // Register all repositories
            services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped(typeof(IClientRequestRepository<>), typeof(ClientRequestRepository<>));
        }

        private void InitDbContextOption(IConfiguration config, DbContextOptionsBuilder options)
        {
            var connectionString = config.GetConnectionString("Default");

            var builder = options.UseSqlServer(connectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });

            if (config["ASPNETCORE_ENVIRONMENT"] == "Development")
            {
                builder.EnableSensitiveDataLogging()
                    // throw an exception when you are evaluating a query in-memory instead of in SQL, for performance
                    .ConfigureWarnings(x => x.Throw(RelationalEventId.QueryClientEvaluationWarning));
            }
        }
    }
}
