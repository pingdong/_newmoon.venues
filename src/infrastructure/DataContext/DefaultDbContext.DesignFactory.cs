using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PingDong.CleanArchitect.Core;

namespace PingDong.Newmoon.Places.Infrastructure
{
    /// <summary>
    /// This class is used in database migration of EF Core
    /// </summary>

    public class DefaultDbContextDesignFactory : IDesignTimeDbContextFactory<DefaultDbContext>
    {
        public DefaultDbContext CreateDbContext(string[] args)
        {
            // Configuration
            var settingFile = Path.Combine(Directory.GetCurrentDirectory(), @"..\Places.Api", "local.settings.json");
                
            var config = new ConfigurationBuilder()
                                .AddJsonFile(settingFile, optional: true, reloadOnChange: true)
                                .Build();
            var connectionString = config["ConnectionStrings:Default"];

            var optionsBuilder = new DbContextOptionsBuilder<DefaultDbContext>()
                .UseSqlServer(connectionString);

            return new DefaultDbContext(optionsBuilder.Options, new EmptyMediator());
        }
    }
}
