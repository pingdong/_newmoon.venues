using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PingDong.CleanArchitect.Infrastructure.SqlServer;
using PingDong.CleanArchitect.Service;
using PingDong.Newmoon.Places.Core;
using PingDong.Newmoon.Places.Infrastructure.EntityConfigurations;

namespace PingDong.Newmoon.Places.Infrastructure
{
    public class DefaultDbContext : GenericDbContext<Guid>
    {
        public const string DefaultSchema = "dbo";

        private readonly ITenantProvider<string> _tenant;

        public DefaultDbContext(DbContextOptions options, IMediator mediator, ITenantProvider<string> tenantProvider) : base(options, mediator)
        {
            _tenant = tenantProvider ?? throw new ArgumentNullException(nameof(tenantProvider));
        }
        
        public DbSet<ClientRequest<Guid>> Requests { get; set; }
        public DbSet<Place> Places { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PlaceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration<Guid>());
            
            modelBuilder.Entity<Place>().HasQueryFilter(p => p.TenantId == _tenant.GetTenantId());
            modelBuilder.Entity<ClientRequest<Guid>>().HasQueryFilter(p => p.TenantId == _tenant.GetTenantId());
        }  
    }
}
