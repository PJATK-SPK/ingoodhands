using Core.Database.Config.Base;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Core
{
    public class OrderConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : Order
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);

            builder.ToTable("orders", "core");
            builder.Property(c => c.AddressId).IsRequired();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(9);
            builder.Property(c => c.Percentage).IsRequired();
            builder.Property(c => c.OwnerUserId).IsRequired();
            builder.Property(c => c.CreationDate).IsRequired();
            builder.Property(c => c.IsCanceledByUser).IsRequired();
            builder.Property(c => c.IsFinished).IsRequired();

            builder.HasIndex(c => c.Name).IsUnique().HasDatabaseName("order_name_idx");
            builder.HasIndex(c => c.Percentage).HasDatabaseName("percentage_idx");

            builder.HasOne(c => c.Address).WithMany(c => (IEnumerable<TBase>?)c.Orders).HasForeignKey(c => c.AddressId).HasConstraintName("address_orders_id_fkey");
            builder.HasOne(c => c.OwnerUser).WithMany(c => (IEnumerable<TBase>?)c.Orders).HasForeignKey(c => c.OwnerUserId).HasConstraintName("orders_owner_users_id_fkey");
        }
    }
}
