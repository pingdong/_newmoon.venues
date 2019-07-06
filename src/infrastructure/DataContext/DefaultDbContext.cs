using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PingDong.Newmoon.Places.Core;
using PingDong.CleanArchitect.Infrastructure.SqlServer;
using PingDong.CleanArchitect.Infrastructure.SqlServer.Idempotency;
using PingDong.CleanArchitect.Service;
using PingDong.Newmoon.Places.Infrastructure.EntityConfigurations;

namespace PingDong.Newmoon.Places.Infrastructure
{
    public class DefaultDbContext : GenericDbContext<Guid>
    {
        public const string DefaultSchema = "dbo";
        
        // Places
        public DbSet<Place> Places { get; set; }

        // Requests
        public DbSet<ClientRequest<Guid>> Requests { get; set; }

        private DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base (options) { }

        public DefaultDbContext(DbContextOptions<DefaultDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Place
            modelBuilder.ApplyConfiguration(new PlaceEntityTypeConfiguration());

            // Client Requests
            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration<Guid>());
        }  
    }
}
