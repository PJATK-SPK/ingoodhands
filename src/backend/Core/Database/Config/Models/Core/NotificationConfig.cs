using Core.Database.Config.Base;

using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Core
{
    public class NotificationConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : Notification
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("notifications", "core");

            builder.Property(c => c.Message).IsRequired();
            builder.HasOne(c => c.User).WithMany(c => (IEnumerable<TBase>?)c.Notifications).HasForeignKey(c => c.UserId).HasConstraintName("notifications_user_id_fkey");
        }
    }
}
