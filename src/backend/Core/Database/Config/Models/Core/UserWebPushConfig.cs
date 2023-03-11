using Core.Database.Config.Base;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Core
{
    public class UserWebPushConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : UserWebPush
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("users_webpush", "core");

            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.Auth).IsRequired().HasMaxLength(50);
            builder.Property(c => c.P256dh).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Endpoint).IsRequired().HasMaxLength(350);

            builder.HasOne(c => c.User).WithOne(c => (TBase?)c.UserWebPush).HasForeignKey<UserWebPush>(c => c.UserId).HasConstraintName("users_webpush_user_id_fkey");
        }
    }
}