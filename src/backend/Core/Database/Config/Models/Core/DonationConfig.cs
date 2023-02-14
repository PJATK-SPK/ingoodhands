using Core.Database.Config.Base;
using Core.Database.Enums;
using Core.Database.Extensions;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Core
{
    public class DonationConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : Donation
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("donations", "core");
            builder.Property(c => c.CreationUserId).IsRequired();
            builder.Property(c => c.CreationDate).IsRequired();
            builder.Property(c => c.WarehouseId).IsRequired();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(9);
            builder.Property(c => c.IsExpired).IsRequired();
            builder.Property(c => c.IsDelivered).IsRequired();
            builder.Property(c => c.IsIncludedInStock).IsRequired();
            
            builder.HasOne(c => c.Warehouse).WithMany(c => (IEnumerable<TBase>?)c.Donations).HasForeignKey(c => c.WarehouseId).HasConstraintName("donations_warehouses_id_fkey");
            builder.HasOne(c => c.CreationUser).WithMany(c => (IEnumerable<TBase>?)c.Donations).HasForeignKey(c => c.CreationUserId).HasConstraintName("donations_creation_users_id_fkey");
        }
    }
}

