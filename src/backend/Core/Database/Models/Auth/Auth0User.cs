using Core.Database.Base;
using Core.Database.Config.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Auth
{
    public class Auth0User : DbEntity, IEntityTypeConfiguration<Auth0User>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Nickname { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; } = default!;
        public string Identifier { get; set; } = default!;
        public User? User { get; set; }
        public long UserId { get; set; } = default!;

        public void Configure(EntityTypeBuilder<Auth0User> builder)
            => new Auth0UserConfig<Auth0User>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(
                HashCode.Combine(base.GetHashCode(), FirstName),
                HashCode.Combine(LastName, Nickname, Email, Identifier, UserId)
               );
    }
}
