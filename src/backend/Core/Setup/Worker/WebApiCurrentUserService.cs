using Core.Database.Seeders;
using Core.Setup.Auth0;

namespace Core.Setup.WebApi.Worker
{
    public class WorkerCurrentUserService : ICurrentUserService
    {
        private readonly CurrentUserInfo _currentUserInfo = new()
        {
            Email = UserSeeder.ServierUser.Email,
            EmailVerified = true,
            FamilyName = UserSeeder.ServierUser.LastName,
            GivenName = UserSeeder.ServierUser.FirstName,
            Locale = "pl-PL",
            Name = UserSeeder.ServierUser.FirstName,
            Nickname = UserSeeder.ServierUser.FirstName,
            PictureURL = "",
            Identifier = UserSeeder.ServierUser.FirstName,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
        };

        public string GetUserAuthIdentifier() => "Service";

        public string GetUserEmail() => UserSeeder.ServierUser.Email;

        public Task<CurrentUserInfo> GetUserInfo() => Task.FromResult(_currentUserInfo);
    }
}
