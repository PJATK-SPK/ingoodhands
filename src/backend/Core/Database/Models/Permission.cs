using Core.Database.Base;
using Core.Database.Config.Models;
using Core.Database.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models
{
    public class Permission : DbEntity, IEntityTypeConfiguration<Permission>
    {
        public PermissionName Name { get; set; }
        public List<RolePermission>? RolePermissions { get; set; }

        public void Configure(EntityTypeBuilder<Permission> builder)
           => new PermissionConfig<Permission>().Configure(builder);

        public override bool Equals(object? obj) => obj is User;

        public override int GetHashCode()
            => HashCode.Combine(base.GetHashCode(), Name);
    }
}
