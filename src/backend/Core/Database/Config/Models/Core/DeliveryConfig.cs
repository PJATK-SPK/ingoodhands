using Core.Database.Config.Base;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Core
{
    public class DeliveryConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : Delivery
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);

            builder.ToTable("deliveries", "core");

            builder.Property(c => c.Name).IsRequired().HasMaxLength(9);
            builder.Property(c => c.OrderId).IsRequired();
            builder.Property(c => c.IsDelivered).IsRequired();
            builder.Property(c => c.DelivererUserId).IsRequired();
            builder.Property(c => c.CreationDate).IsRequired();
            builder.Property(c => c.IsLost).IsRequired();
            builder.Property(c => c.IsPacked).IsRequired();
            builder.Property(c => c.WarehouseId).IsRequired();

            builder.HasOne(c => c.Warehouse).WithMany(c => (IEnumerable<TBase>?)c.Deliveries).HasForeignKey(c => c.WarehouseId).HasConstraintName("deliveries_warehouses_id_fkey");
            builder.HasOne(c => c.DelivererUser).WithMany(c => (IEnumerable<TBase>?)c.Deliveries).HasForeignKey(c => c.DelivererUserId).HasConstraintName("deliveries_deliverer_users_id_fkey");
        }
    }
}
