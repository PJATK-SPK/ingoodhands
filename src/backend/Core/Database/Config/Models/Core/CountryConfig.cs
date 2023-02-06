using Core.Database.Config.Base;
using Core.Database.Extensions;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Core
{
    public class CountryConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : Country
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("countries", "core");

            builder.Property(c => c.EnglishName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Alpha3IsoCode).IsRequired().HasMaxLength(3);
            builder.Property(c => c.Alpha2IsoCode).IsRequired().HasMaxLength(2);

            builder.HasUniqueConstraint(c => c.Alpha3IsoCode);
            builder.HasUniqueConstraint(c => c.Alpha2IsoCode);
        }
    }
}