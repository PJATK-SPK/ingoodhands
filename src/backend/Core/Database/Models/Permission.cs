using Core.Database.Base;
using Core.Database.Config.Models;
using Core.Database.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models
{
    public class Permission : DbEntity, IEntityTypeConfiguration<Permission>
    {
        public PermissionTopic Topic { get; set; } = default!;
        //public List<UserPermission>? UserPermissions { get; set; }

        public void Configure(EntityTypeBuilder<Permission> builder)
            => new PermissionConfig<Permission>().Configure(builder);

        public override bool Equals(object? obj)
            => obj is Permission compare &&
                Topic == compare.Topic;

        public override int GetHashCode()
            => HashCode.Combine(base.GetHashCode(), Topic);
    }
}
