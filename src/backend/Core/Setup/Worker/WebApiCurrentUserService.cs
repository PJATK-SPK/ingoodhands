using Core.Database.Seeders;
using Core.Setup.Auth0;

namespace Core.Setup.WebApi.Worker
{
    public class WorkerCurrentUserService : ICurrentUserService
    {
        private readonly CurrentUserInfo _currentUserInfo = new()
        {
            Email = UserSeeder.ServiceUser.Email,
            EmailVerified = true,
            FamilyName = UserSeeder.ServiceUser.LastName,
            GivenName = UserSeeder.ServiceUser.FirstName,
            Locale = "pl-PL",
            Name = UserSeeder.ServiceUser.FirstName,
            Nickname = UserSeeder.ServiceUser.FirstName,
            PictureURL = "",
            Identifier = UserSeeder.ServiceUser.FirstName,
            UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
        };

        public string GetUserAuthIdentifier() => "Service";

        public string GetUserEmail() => UserSeeder.ServiceUser.Email;

        public Task<CurrentUserInfo> GetUserInfo() => Task.FromResult(_currentUserInfo);
    }
}
