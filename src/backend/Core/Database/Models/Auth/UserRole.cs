using Core.Database.Base;
using Core.Database.Config.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Auth
{
    public class UserRole : DbEntity, IEntityTypeConfiguration<UserRole>
    {
        public Role? Role { get; set; }
        public long RoleId { get; set; }
        public User? User { get; set; }
        public long UserId { get; set; }

        public void Configure(EntityTypeBuilder<UserRole> builder)
           => new UserRoleConfig<UserRole>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(base.GetHashCode(), RoleId, UserId);
    }
}
