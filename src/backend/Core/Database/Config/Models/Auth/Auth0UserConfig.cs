using Core.Database.Config.Base;
using Core.Database.Extensions;
using Core.Database.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Auth
{
    public class Auth0UserConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : Auth0User
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("auth0_users", "auth");
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.LastName).HasMaxLength(50);
            builder.Property(c => c.Nickname).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(254);
            builder.Property(c => c.Identifier).IsRequired().HasMaxLength(80);
            builder.HasUniqueConstraint(c => c.Identifier);
            builder.Property(c => c.UserId).IsRequired();
            builder.HasOne(c => c.User).WithMany(c => (IEnumerable<TBase>?)c.Auth0Users).HasForeignKey(c => c.UserId).HasConstraintName("auth_users_user_id_fkey");
            builder.HasIndex(c => new { c.Status, c.Identifier }).HasDatabaseName("auth_users_status_identifier_idx");
        }
    }
}
