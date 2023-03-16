using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Core.Database.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class Order : DbEntity, IEntityTypeConfiguration<Order>
    {
        public long AddressId { get; set; }
        public Address? Address { get; set; }
        public string Name { get; set; } = default!;
        public int Percentage { get; set; }
        public long OwnerUserId { get; set; }
        public User? OwnerUser { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsCanceledByUser { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }
        public List<Delivery>? Deliveries { get; set; }

        public void Configure(EntityTypeBuilder<Order> builder)
            => new OrderConfig<Order>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(
                base.GetHashCode(),
                HashCode.Combine(AddressId, Name, Percentage, OwnerUserId, CreationDate, IsCanceledByUser)
                );
    }
}
