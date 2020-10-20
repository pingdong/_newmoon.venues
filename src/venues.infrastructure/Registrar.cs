using Microsoft.Extensions.DependencyInjection;
using PingDong.CQRS.Infrastructure;

namespace PingDong.Newmoon.Venues.Infrastructure
{
    public static class Registrar
    {
        public static void Register(this IServiceCollection services)
        {
            // Memory Cache
            services.AddSingleton<ICache, MemoryCache>();

            // Azure Storage
            services.AddSingleton<IRepository<,>, AzureBlobStorageRepository>();
            services.AddSingleton<IRepository, AzureTableStorageRepository>();

            // Azure Sql
        }
    }
}
