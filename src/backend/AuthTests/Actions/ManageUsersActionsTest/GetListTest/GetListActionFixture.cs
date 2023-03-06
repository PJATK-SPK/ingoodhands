using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthTests.Actions.ManageUsersActionsTest.GetListTest
{
    public static class GetListActionFixture
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

        public static UserRole CreateUserRole(User user, long roleId) => new()
        {
            RoleId = roleId,
            User = user,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Status = DbEntityStatus.Active
        };
    }
}
