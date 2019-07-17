using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PingDong.Newmoon.Places.Core;

namespace PingDong.Newmoon.Places.Infrastructure.EntityConfigurations
{
    class PlaceEntityTypeConfiguration
        : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> cfg)
        {
            // Table
            cfg.ToTable("Places", DefaultDbContext.DefaultSchema);
            
            // Base Entity
            //    Primary Key
            cfg.HasKey(o => o.Id);
            //    Columns
            cfg.Property(o => o.Id)
                .HasColumnName("Id");
            cfg.Property(o => o.TenantId)
                .HasColumnName("TenantId")
                // Ignore TenantId if this is a single tenant application
                .IsRequired(); 
            //    Index
            cfg.HasIndex(p => new { p.Id, p.TenantId });
            //   Ignore
            cfg.Ignore(b => b.CorrelationId);
            cfg.Ignore(b => b.DomainEvents);

            // Properties
            cfg.Property(o => o.Name)
                .HasColumnName("PlaceName")
                .HasMaxLength(200)
                .IsRequired();
            
            cfg.Ignore(b => b.State);
            cfg.Property("_placeStateId")
                .HasColumnName("StateId")
                .IsRequired();

            cfg.OwnsOne(o => o.Address, b =>
            {
                b.Property(p => p.No)
                    .HasColumnName("AddressNo")
                    .HasMaxLength(20)
                    .IsRequired();

                b.Property(s => s.Street)
                    .HasColumnName("AddressStreet")
                    .HasMaxLength(100)
                    .IsRequired();

                b.Property(s => s.City)
                    .HasColumnName("AddressCity")
                    .HasMaxLength(40)
                    .IsRequired();

                b.Property(s => s.State)
                    .HasColumnName("AddressState")
                    .HasMaxLength(40)
                    .IsRequired();

                b.Property(s => s.Country)
                    .HasColumnName("AddressCountry")
                    .HasMaxLength(40)
                    .IsRequired();

                b.Property(s => s.ZipCode)
                    .HasColumnName("AddressZipCode")
                    .HasMaxLength(10)
                    .IsRequired();
            });
        }
    }
}
