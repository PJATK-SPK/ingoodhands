using Core.Database;
using Core.Database.Models.Auth;
using Core.Exceptions;
using Core.Setup.Auth0;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace Auth.Actions.UserSettingsActions.GetUserDetails
{
    public class GetUserDetailsService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<GetUserDetailsService> _logger;

        public GetUserDetailsService(AppDbContext appDbContext, ILogger<GetUserDetailsService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<User> GetCurrentUser(CurrentUserInfo auth0UserInfo)
        {
            var userFromDatabase = await _appDbContext.Users
                .Include(c => c.Auth0Users!)
                .Include(c => c.Roles!).ThenInclude(c => c.Role!)
                .Where(c => c.Auth0Users!.Any(s => s.Identifier == auth0UserInfo.Identifier))
                .SingleOrDefaultAsync();

            if (userFromDatabase == null)
            {
                _logger.LogError("User with email {email} was not found in table Auth0Users", auth0UserInfo.Email);
                throw new ApplicationErrorException("Sorry we couldn't find your user in database");
            }

            return userFromDatabase!;
        }
    }
}