using Core.Database.Config.Base;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Core
{
    internal class OrderProductConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : OrderProduct
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);

            builder.ToTable("order_products", "core");
            builder.Property(c => c.OrderId).IsRequired();
            builder.Property(c => c.ProductId).IsRequired();
            builder.Property(c => c.Quantity).IsRequired();

            builder.HasOne(c => c.Order).WithMany(c => (IEnumerable<TBase>?)c.OrderProducts).HasForeignKey(c => c.OrderId).HasConstraintName("order_products_id_fkey");
            builder.HasOne(c => c.Product).WithMany(c => (IEnumerable<TBase>?)c.OrderProducts).HasForeignKey(c => c.ProductId).HasConstraintName("product_orders_id_fkey");
        }
    }
}
