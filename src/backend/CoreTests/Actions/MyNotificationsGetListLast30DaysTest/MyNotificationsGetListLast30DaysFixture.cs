using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Database.Seeders;

namespace CoreTests.Actions.MyNotificationsGetListLast30DaysTest
{
    public static class MyNotificationsGetListLast30DaysFixture
    {
        public static User CreateUser(string firstName, string lastName) => new()
        {
            Status = DbEntityStatus.Active,
            FirstName = firstName,
            LastName = lastName,
            Email = firstName + "@" + lastName + ".com",
        };

        public static Auth0User CreateAuth0User(User user, int id) => new()
        {
            FirstName = "Auth",
            LastName = "Auth0User" + id,
            Nickname = "Auth0",
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Email = user.Email,
            Identifier = user.FirstName + user.LastName + "GoogleId",
            User = user,
            UserId = user.Id
        };

        public static Notification CreateNotifaction(User user, long daysPastFromToday, string partMessage) => new()
        {
            UserId = user.Id,
            User = user,
            CreationDate = DateTime.UtcNow.AddDays(-daysPastFromToday),
            Message = "You've reached level 10 donor! " + partMessage,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
