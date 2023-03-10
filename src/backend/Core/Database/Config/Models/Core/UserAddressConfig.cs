using Core.Database.Config.Base;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Core
{
    public class UserAddressConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : UserAddress
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("user_addresses", "core");

            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.AddressId).IsRequired();
            builder.Property(c => c.IsDeletedByUser).IsRequired();

            builder.HasOne(c => c.User).WithMany(c => (IEnumerable<TBase>?)c.UserAddresses).HasForeignKey(c => c.UserId).HasConstraintName("user_addresses_user_id_fkey");
        }
    }
}