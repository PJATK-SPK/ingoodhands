using Core.Database.Config.Models;
using Core.Database.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models
{
    public class User : IEntityTypeConfiguration<User>
    {
        public long Id { get; set; }
        public DbEntityStatus Status { get; set; }
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        public string Email { get; set; } = default!;
        public List<Auth0User>? Auth0Users { get; set; }

        public void Configure(EntityTypeBuilder<User> builder)
            => new UserConfig<User>().Configure(builder);

        public override bool Equals(object? obj) => obj is User;

        public override int GetHashCode()
            => HashCode.Combine(
                HashCode.Combine(base.GetHashCode(), FirstName),
                HashCode.Combine(LastName, Email)
               );
    }
}
