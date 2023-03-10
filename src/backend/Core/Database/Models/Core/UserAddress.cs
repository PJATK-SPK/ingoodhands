using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Core.Database.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class UserAddress : DbEntity, IEntityTypeConfiguration<UserAddress>
    {
        public long UserId { get; set; }
        public User? User { get; set; }
        public long AddressId { get; set; }
        public Address? Address { get; set; }
        public bool IsDeletedByUser { get; set; }

        public void Configure(EntityTypeBuilder<UserAddress> builder)
               => new UserAddressConfig<UserAddress>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(
                base.GetHashCode(),
                HashCode.Combine(UserId, AddressId, IsDeletedByUser)
                );
    }
}
