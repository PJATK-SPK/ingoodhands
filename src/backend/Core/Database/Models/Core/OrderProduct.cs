using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class OrderProduct : DbEntity, IEntityTypeConfiguration<OrderProduct>
    {
        public long OrderId { get; set; }
        public Order? Order { get; set; }
        public long ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }

        public void Configure(EntityTypeBuilder<OrderProduct> builder)
           => new OrderProductConfig<OrderProduct>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(base.GetHashCode(), OrderId, ProductId, Quantity);
    }
}