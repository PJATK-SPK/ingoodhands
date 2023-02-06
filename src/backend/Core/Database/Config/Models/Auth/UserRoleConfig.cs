using Core.Database.Config.Base;
using Core.Database.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Auth
{
    public class UserRoleConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : UserRole
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("user_roles", "core");

            builder.Property(c => c.RoleId).IsRequired();
            builder.HasOne(c => c.Role).WithMany(c => (IEnumerable<TBase>?)c.Users).HasForeignKey(c => c.RoleId).HasConstraintName("user_roles_role_id_fkey");

            builder.Property(c => c.UserId).IsRequired();
            builder.HasOne(c => c.User).WithMany(c => (IEnumerable<TBase>?)c.Roles).HasForeignKey(c => c.UserId).HasConstraintName("user_roles_user_id_fkey");
        }
    }
}