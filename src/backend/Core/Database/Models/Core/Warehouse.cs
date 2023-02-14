using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class Warehouse : DbEntity, IEntityTypeConfiguration<Warehouse>
    {
        public long AddressId { get; set; }
        public Address? Address { get; set; }
        public string ShortName { get; set; } = default!;
        
        public List<Donation>? Donations { get; set; }
        
        public void Configure(EntityTypeBuilder<Warehouse> builder)
            => new WarehouseConfig<Warehouse>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(
                base.GetHashCode(),
                HashCode.Combine(AddressId, ShortName)
                );
    }
}
