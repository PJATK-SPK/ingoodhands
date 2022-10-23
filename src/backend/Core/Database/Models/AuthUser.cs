using Core.Database.Base;
using Core.Database.Config.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models
{
    public class AuthUser : DbEntity, IEntityTypeConfiguration<AuthUser>
    {
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        public string Nickname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Identifier { get; set; } = default!;
        public User User { get; set; } = default!;
        public long UserId { get; set; } = default!;

        public void Configure(EntityTypeBuilder<AuthUser> builder)
            => new AuthUserConfig<AuthUser>().Configure(builder);

        public override bool Equals(object? obj)
            => obj is AuthUser compare &&
                FirstName == compare.FirstName &&
                LastName == compare.LastName &&
                Nickname == compare.Nickname &&
                Email == compare.Email &&
                UserId == compare.UserId &&
                Identifier == compare.Identifier;

        public override int GetHashCode()
            => HashCode.Combine(
                HashCode.Combine(base.GetHashCode(), FirstName),
                HashCode.Combine(LastName, Nickname, Email, Identifier, UserId)
               );
    }
}
