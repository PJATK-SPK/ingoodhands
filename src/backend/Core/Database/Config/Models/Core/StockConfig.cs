using Core.Database.Config.Base;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Core
{
    public class StockConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : Stock
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("stocks", "core");

            builder.Property(c => c.ProductId).IsRequired();
            builder.Property(c => c.Quantity).IsRequired();
            builder.Property(c => c.WarehouseId).IsRequired();

            builder.HasOne(c => c.Product).WithMany(c => (IEnumerable<TBase>?)c.Stocks).HasForeignKey(c => c.ProductId).HasConstraintName("stocks_product_id_fkey");
            builder.HasOne(c => c.Warehouse).WithMany(c => (IEnumerable<TBase>?)c.Stocks).HasForeignKey(c => c.WarehouseId).HasConstraintName("stocks_warehouse_id_fkey");
        }
    }
}
