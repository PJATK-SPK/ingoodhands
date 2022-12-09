using Core.Auth0;
using Core.Database;
using Core.Database.Models;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic.PostLogin
{
    public class UserCreationService
    {
        private readonly AppDbContext _appDbContext;

        public UserCreationService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User?> CreateUserAndAddToDatabase(CurrentUserInfo auth0UserInfo)
        {
            var user = await _appDbContext.Users.SingleOrDefaultAsync(c => c.Email == auth0UserInfo.Email);
            var auth0UserFromDatabase = await _appDbContext.Auth0Users.SingleOrDefaultAsync(c => c.Identifier == auth0UserInfo.Identifier);

            var serviceUser = await _appDbContext.Users.SingleOrDefaultAsync(c => c.Email == DbConstants.ServiceUserEmail);
            if (serviceUser == null)
            {
                throw new SingleOrDefaultException("ServiceUser in PostLoginAction didn't find serviceUser in database table Users");
            }

            if (user == null && auth0UserFromDatabase == null)
            {
                user = CreateUser(auth0UserInfo);
                _appDbContext.Add(user);

                var newAuth0User = CreateAuth0User(auth0UserInfo, user, serviceUser!);
                _appDbContext.Add(newAuth0User);
                _appDbContext.SaveChanges();

            }
            else if (user != null && auth0UserFromDatabase == null)
            {
                var newAuth0User = CreateAuth0User(auth0UserInfo, user, serviceUser!);
                _appDbContext.Add(newAuth0User);
                _appDbContext.SaveChanges();
            }
            else if (user == null && auth0UserFromDatabase != null)
            {
                user = CreateUser(auth0UserInfo);
                _appDbContext.Add(user);
                _appDbContext.SaveChanges();
            }

            return user;
        }
        private User CreateUser(CurrentUserInfo currentAuth0UserInfo)
        {
            return new User
            {
                Status = Core.Database.Enums.DbEntityStatus.Active,
                FirstName = currentAuth0UserInfo.GivenName!,
                LastName = currentAuth0UserInfo.FamilyName,
                Email = currentAuth0UserInfo.Email!
            };
        }

        private Auth0User CreateAuth0User(CurrentUserInfo currentAuth0UserInfo, User user, User serviceUser)
        {
            return new Auth0User
            {
                FirstName = currentAuth0UserInfo.GivenName!,
                LastName = currentAuth0UserInfo.FamilyName,
                Nickname = currentAuth0UserInfo.Nickname!,
                UpdateUser = serviceUser,
                UpdateUserId = serviceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Email = currentAuth0UserInfo.Email!,
                Identifier = currentAuth0UserInfo.Identifier!,
                User = user,
                UserId = user.Id
            };
        }
    }
}
