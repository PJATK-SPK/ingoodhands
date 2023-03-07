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
        }
    }
}
