using Core.Database.Base;
using Core.Database.Config.Models;
using Core.Database.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models
{
    public class Role : DbEntity, IEntityTypeConfiguration<Role>
    {
        public RoleName Name { get; set; }
        public List<RolePermission>? RolePermissions { get; set; }

        public void Configure(EntityTypeBuilder<Role> builder)
           => new RoleConfig<Role>().Configure(builder);

        public override bool Equals(object? obj) => obj is User;

        public override int GetHashCode()
            => HashCode.Combine(base.GetHashCode(), Name);
    }
}
