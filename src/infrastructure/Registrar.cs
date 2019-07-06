using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PingDong.CleanArchitect.Infrastructure;
using PingDong.CleanArchitect.Infrastructure.SqlServer;

namespace PingDong.Newmoon.Places.Infrastructure
{
    public class Registrar
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("Default");

            services.AddDbContext<DefaultDbContext>(options =>
                {
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
            );

            // Register all repositories
            services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));
        }
    }
}
