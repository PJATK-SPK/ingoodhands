using Core.Database.Config.Base;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Base
{
    public class DbEntity
    {
        public long Id { get; set; }
        public User? UpdateUser { get; set; }
        public long? UpdateUserId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DbEntityStatus Status { get; set; }

        public static void Configure(EntityTypeBuilder<DbEntity> builder)
            => new DbEntityConfig<DbEntity>().Configure(builder);

        public override bool Equals(object? obj)
        {
            return obj is DbEntity entity &&
                   Id == entity.Id &&
                   UpdateUserId == entity.UpdateUserId &&
                   UpdatedAt == entity.UpdatedAt &&
                   Status == entity.Status;
        }

        public override int GetHashCode()
            => HashCode.Combine(Id, UpdateUserId, UpdatedAt, Status);
    }
}
