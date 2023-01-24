using Core.Auth0;
using Core.Database;
using Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic.UserSettings
{
    public class UserSettingsService
    {
        private readonly AppDbContext _appDbContext;

        public UserSettingsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Auth0User>> GetAllAuth0UsersFromUser(CurrentUserInfo auth0UserInfo)
        {
            var auth0UserFromDatabase = await _appDbContext.Auth0Users.SingleOrDefaultAsync(c => c.Identifier == auth0UserInfo.Identifier);
            var auth0UserUserId = auth0UserFromDatabase!.UserId;
            var currentUserAuth0UsersList = await _appDbContext.Auth0Users.Where(c => c.UserId == auth0UserUserId).ToListAsync();

            return currentUserAuth0UsersList;
        }
    }
}
