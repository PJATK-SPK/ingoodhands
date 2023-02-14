using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class DonationProduct : DbEntity, IEntityTypeConfiguration<DonationProduct>
    {
        public long DonationId { get; set; }
        public Donation? Donation { get; set; }
        public long ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }

        public void Configure(EntityTypeBuilder<DonationProduct> builder)
           => new DonationProductConfig<DonationProduct>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(base.GetHashCode(), DonationId, ProductId, Quantity);
    }
}
