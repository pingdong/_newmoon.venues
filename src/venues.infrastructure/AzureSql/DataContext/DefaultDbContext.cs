using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PingDong.CleanArchitect.Service;
using PingDong.Newmoon.Venues.Core;
using PingDong.CleanArchitect.Infrastructure.SqlServer;
using PingDong.Newmoon.Venues.Infrastructure.EntityConfigurations;

namespace PingDong.Newmoon.Venues.Infrastructure
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
        public DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new VenueEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration<Guid>());
            
            modelBuilder.Entity<Venue>().HasQueryFilter(p => p.TenantId == _tenantProvider.GetTenantId());
            modelBuilder.Entity<ClientRequest<Guid>>();
        }  
    }
}
