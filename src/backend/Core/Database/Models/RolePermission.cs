using Core.Database.Base;
using Core.Database.Config.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models
{
    public class RolePermission : DbEntity, IEntityTypeConfiguration<RolePermission>
    {
        public Role Role { get; set; } = default!;
        public long RoleId { get; set; } = default!;
        public Permission Permission { get; set; } = default!;
        public long PermissionId { get; set; } = default!;

        public void Configure(EntityTypeBuilder<RolePermission> builder)
           => new RolePermissionConfig<RolePermission>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(base.GetHashCode(), RoleId, PermissionId);
    }
}
