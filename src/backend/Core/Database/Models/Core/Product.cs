using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Core.Database.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class Product : DbEntity, IEntityTypeConfiguration<Product>
    {
        public string Name { get; set; } = default!;
        public UnitType Unit { get; set; }
        public List<DonationProduct>? DonationProducts { get; set; }
        public List<Stock>? Stocks { get; set; }

        public void Configure(EntityTypeBuilder<Product> builder)
           => new ProductConfig<Product>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(base.GetHashCode(), Name, Unit);
    }
}
