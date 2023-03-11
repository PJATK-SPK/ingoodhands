using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Core.Database.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class UserWebPush : DbEntity, IEntityTypeConfiguration<UserWebPush>
    {
        public long UserId { get; set; }
        public User? User { get; set; }
        public string Endpoint { get; set; } = default!;
        public string Auth { get; set; } = default!;
        public string P256dh { get; set; } = default!;

        public void Configure(EntityTypeBuilder<UserWebPush> builder)
               => new UserWebPushConfig<UserWebPush>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(
                base.GetHashCode(),
                HashCode.Combine(UserId, Endpoint, Auth, P256dh)
                );
    }
}
