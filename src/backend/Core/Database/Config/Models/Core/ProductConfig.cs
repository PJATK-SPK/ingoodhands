using Core.Database.Config.Base;
using Core.Database.Enums;
using Core.Database.Extensions;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Core
{
    public class ProductConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : Product
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("products", "core");
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Unit).IsRequired().HasMaxLength(25).HasConversion(c => c.ToString(), c => Enum.Parse<Unit>(c!));
            builder.HasUniqueConstraint(c => c.Name);
        }
    }
}