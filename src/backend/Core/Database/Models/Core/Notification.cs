using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Core.Database.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class Notification : DbEntity, IEntityTypeConfiguration<Notification>
    {
        public long UserId { get; set; }
        public User? User { get; set; }
        public DateTime CreationDate { get; set; }
        public string Message { get; set; } = default!;

        public void Configure(EntityTypeBuilder<Notification> builder)
             => new NotificationConfig<Notification>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(base.GetHashCode(), UserId, CreationDate, Message);
    }
}
