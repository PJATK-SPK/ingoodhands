using Core.Database.Config.Base;
using Core.Database.Extensions;
using Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Core
{
    public class AddressConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : Address
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("addresses", "core");

            builder.Property(c => c.PostalCode).IsRequired().HasMaxLength(10);
            builder.Property(c => c.City).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Street).HasMaxLength(100);
            builder.Property(c => c.StreetNumber).HasMaxLength(10);
            builder.Property(c => c.Apartment).HasMaxLength(10);
            builder.Property(c => c.GpsLatitude).IsRequired();
            builder.Property(c => c.GpsLongitude).IsRequired();

            builder.HasOne(c => c.Country).WithMany(c => (IEnumerable<TBase>?)c.Addresses).HasForeignKey(c => c.CountryId).HasConstraintName("addresses_country_id_fkey");
        }
    }
}