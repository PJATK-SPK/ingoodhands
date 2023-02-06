using Core.Database.Config.Base;
using Core.Database.Enums;
using Core.Database.Extensions;
using Core.Database.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Auth
{
    public class RoleConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : Role
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("roles", "core");
            builder.Property(c => c.Name).IsRequired().HasMaxLength(25).HasConversion(c => c.ToString(), c => Enum.Parse<RoleName>(c!));
            builder.HasUniqueConstraint(c => c.Name);
        }
    }
}