using Core.Database.Base;
using Core.Database.Config.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models
{
    public class User : DbEntity, IEntityTypeConfiguration<User>
    {
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        public string Email { get; set; } = default!;
        public string Locale { get; set; } = default!;
        public List<AuthUser>? AuthUsers { get; set; }

        public void Configure(EntityTypeBuilder<User> builder)
            => new UserConfig<User>().Configure(builder);

        public override bool Equals(object? obj)
            => obj is User compare &&
                FirstName == compare.FirstName &&
                LastName == compare.LastName &&
                Email == compare.Email &&
                Locale == compare.Locale;

        public override int GetHashCode()
            => HashCode.Combine(
                HashCode.Combine(base.GetHashCode(), FirstName),
                HashCode.Combine(LastName, Email, Locale)
               );
    }
}
