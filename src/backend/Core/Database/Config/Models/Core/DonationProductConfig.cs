using Core.Database.Config.Base;
using Core.Database.Enums;
using Core.Database.Extensions;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Core
{
    public class DonationProductConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : DonationProduct
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("donation_products", "core");
            builder.Property(c => c.Quantity).IsRequired();

            builder.HasOne(c => c.Donation).WithMany(c => (IEnumerable<TBase>?)c.Products).HasForeignKey(c => c.DonationId).HasConstraintName("donations_products_id_fkey");

        }
    }
}

