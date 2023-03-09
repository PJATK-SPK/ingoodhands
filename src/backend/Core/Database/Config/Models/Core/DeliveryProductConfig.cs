using Core.Database.Config.Base;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Core
{
    public class DeliveryProductConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : DeliveryProduct
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);

            builder.ToTable("delivery_products", "core");
            builder.Property(c => c.DeliveryId).IsRequired();
            builder.Property(c => c.ProductId).IsRequired();
            builder.Property(c => c.Quantity).IsRequired();

            builder.HasOne(c => c.Delivery).WithMany(c => (IEnumerable<TBase>?)c.DeliveryProducts).HasForeignKey(c => c.DeliveryId).HasConstraintName("delivery_products_id_fkey");
            builder.HasOne(c => c.Product).WithMany(c => (IEnumerable<TBase>?)c.DeliveryProducts).HasForeignKey(c => c.ProductId).HasConstraintName("product_deliveries_id_fkey");
        }
    }
}