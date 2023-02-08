using Core.Database.Config.Base;
using Core.Database.Extensions;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Core
{
    public class WarehouseConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : Warehouse
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("warehouses", "core");

            builder.Property(c => c.ShortName).IsRequired().HasMaxLength(5);
            builder.HasOne(c => c.Address).WithMany(c => (IEnumerable<TBase>?)c.Warehouses).HasForeignKey(c => c.AddressId).HasConstraintName("warehouses_address_id_fkey");
        }
    }
}