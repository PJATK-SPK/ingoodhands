using Core.Database.Config.Base;
using Core.Database.Enums;
using Core.Database.Extensions;
using Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models
{
    public class RolePermissionConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : RolePermission
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("role_permissions", "core");

            builder.Property(c => c.RoleId).IsRequired();
            builder.HasOne(c => c.Role).WithMany(c => (IEnumerable<TBase>?)c.RolePermissions).HasForeignKey(c => c.RoleId).HasConstraintName("role_permissions_role_id_fkey");

            builder.Property(c => c.PermissionId).IsRequired();
            builder.HasOne(c => c.Permission).WithMany(c => (IEnumerable<TBase>?)c.RolePermissions).HasForeignKey(c => c.PermissionId).HasConstraintName("role_permissions_permission_id_fkey");
        }
    }
}