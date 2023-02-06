using Core.Database.Config.Models.Auth;
using Core.Database.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Auth
{
    public class User : IEntityTypeConfiguration<User>
    {
        public long Id { get; set; }
        public DbEntityStatus Status { get; set; }
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        public string Email { get; set; } = default!;
        public List<Auth0User>? Auth0Users { get; set; }
        public List<UserRole>? Roles { get; set; }

        public void Configure(EntityTypeBuilder<User> builder)
            => new UserConfig<User>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(
                HashCode.Combine(Id, Status, FirstName),
                HashCode.Combine(LastName, Email)
               );
    }
}
