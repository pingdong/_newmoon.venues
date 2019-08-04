using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PingDong.CleanArchitect.Service;
using PingDong.Newmoon.Places.Core;
using PingDong.CleanArchitect.Infrastructure.SqlServer;
using PingDong.Newmoon.Places.Infrastructure.EntityConfigurations;

namespace PingDong.Newmoon.Places.Infrastructure
{
    public class DefaultDbContext : GenericDbContext<Guid>
    {
        public const string DefaultSchema = "dbo";

        private readonly ITenantProvider<string> _tenantProvider;

        public DefaultDbContext(DbContextOptions options, IMediator mediator, ITenantProvider<string> tenantProvider) : base(options, mediator)
        {
            _tenantProvider = tenantProvider ?? throw new ArgumentNullException(nameof(tenantProvider));
        }
        
        public DbSet<ClientRequest<Guid>> Requests { get; set; }
        public DbSet<Place> Places { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PlaceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration<Guid>());
            
            modelBuilder.Entity<Place>().HasQueryFilter(p => p.TenantId == _tenantProvider.GetTenantId());
            modelBuilder.Entity<ClientRequest<Guid>>();
        }  
    }
}
