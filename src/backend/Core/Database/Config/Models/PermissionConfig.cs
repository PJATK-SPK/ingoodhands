using Core.Database.Config.Base;
using Core.Database.Enums;
using Core.Database.Extensions;
using Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models
{
    public class PermissionConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : Permission
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("permissions", "core");
            builder.Property(c => c.Topic).IsRequired().HasMaxLength(25).HasConversion(c => c.ToString(), c => Enum.Parse<PermissionTopic>(c!));
            builder.HasUniqueConstraint(c => c.Topic);
        }
    }
}