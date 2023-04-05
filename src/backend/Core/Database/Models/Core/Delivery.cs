using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Core.Database.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class Delivery : DbEntity, IEntityTypeConfiguration<Delivery>
    {
        public string Name { get; set; } = default!;
        public long OrderId { get; set; }
        public Order? Order { get; set; }
        public bool IsDelivered { get; set; }
        public long DelivererUserId { get; set; }
        public User? DelivererUser { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsLost { get; set; }
        public bool TripStarted { get; set; }
        public long WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
        public List<DeliveryProduct>? DeliveryProducts { get; set; }

        public void Configure(EntityTypeBuilder<Delivery> builder)
            => new DeliveryConfig<Delivery>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(
                base.GetHashCode(),
                HashCode.Combine(Name, OrderId, IsDelivered, DelivererUserId, CreationDate, IsLost, TripStarted, WarehouseId)
                );
    }
}
