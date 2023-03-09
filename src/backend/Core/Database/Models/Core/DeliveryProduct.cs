using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class DeliveryProduct : DbEntity, IEntityTypeConfiguration<DeliveryProduct>
    {
        public long DeliveryId { get; set; }
        public Delivery? Delivery { get; set; }
        public long ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }

        public void Configure(EntityTypeBuilder<DeliveryProduct> builder)
           => new DeliveryProductConfig<DeliveryProduct>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(base.GetHashCode(), DeliveryId, ProductId, Quantity);
    }
}
