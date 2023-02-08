using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class Address : DbEntity, IEntityTypeConfiguration<Address>
    {
        public long CountryId { get; set; }
        public Country? Country { get; set; }
        public string PostalCode { get; set; } = default!;
        public string City { get; set; } = default!;
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? Apartment { get; set; }
        public double GpsLatitude { get; set; } = default!;
        public double GpsLongitude { get; set; } = default!;
        
        public List<Warehouse>? Warehouses { get; set; }
            
        public void Configure(EntityTypeBuilder<Address> builder)
            => new AddressConfig<Address>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(
                base.GetHashCode(),
                HashCode.Combine(CountryId, PostalCode, City, Street, StreetNumber, Apartment, GpsLatitude, GpsLongitude)
                );
    }
}
