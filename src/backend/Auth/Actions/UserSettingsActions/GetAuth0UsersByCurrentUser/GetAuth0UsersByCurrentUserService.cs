using Core.Database;
using Core.Database.Models.Auth;
using Core.Exceptions;
using Core.Setup.Auth0;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace Auth.Actions.UserSettingsActions.GetAuth0UsersByCurrentUser
{
    public class GetAuth0UsersByCurrentUserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<GetAuth0UsersByCurrentUserService> _logger;

        public GetAuth0UsersByCurrentUserService(AppDbContext appDbContext, ILogger<GetAuth0UsersByCurrentUserService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<List<Auth0User>> GetAllAuth0UsersFromUser(CurrentUserInfo auth0UserInfo)
        {
            var auth0UserFromDatabase = await _appDbContext.Auth0Users.SingleOrDefaultAsync(c => c.Identifier == auth0UserInfo.Identifier);

            if (auth0UserFromDatabase == null)
            {
                _logger.LogError("User with identifier {identifier} was not found in auth0_users table!", auth0UserInfo.Identifier);
                throw new ApplicationErrorException("Something went wrong, cannot find Auth0User in database");
            }

            var auth0UserUserId = auth0UserFromDatabase!.UserId;
            var currentUserAuth0UsersList = await _appDbContext.Auth0Users.Where(c => c.UserId == auth0UserUserId).ToListAsync();

            return currentUserAuth0UsersList;
        }
    }
}
