using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class Stock : DbEntity, IEntityTypeConfiguration<Stock>
    {
        public long ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }

        public void Configure(EntityTypeBuilder<Stock> builder)
            => new StockConfig<Stock>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(
                base.GetHashCode(),
                HashCode.Combine(ProductId, Quantity)
                );
    }
}