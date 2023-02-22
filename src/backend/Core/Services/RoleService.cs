using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Exceptions;
using Core.Setup.Auth0;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public class RoleService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<RoleService> _logger;
        private readonly GetCurrentUserService _getCurrentUserService;

        public RoleService(ILogger<RoleService> logger, AppDbContext appDbContext, ICurrentUserService currentUserService, GetCurrentUserService getCurrentUserService)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
        }

        public async Task<List<RoleName>> GetRolesAsync(long? userId = null)
        {
            if (userId == null)
            {
                var roleNames = await GetCurrentUserRolesRoleNames();
                return roleNames;
            }

            var roleNames2 = await GetUserRolesRoleNamesFromUserWithId(userId);

            return roleNames2;
        }

        public async Task<bool> HasRole(RoleName role, long? userId = null)
        {
            var listOfUserRolesRoleNames = await GetRolesAsync(userId);

            var userHasGivenRole = listOfUserRolesRoleNames.Any(c => c == role);

            return userHasGivenRole;
        }

        public async Task AssertRole(RoleName roleName, long? userId = null)
        {
            var serviceUser = await _appDbContext.Users.SingleOrDefaultAsync(c => c.Email == DbConstants.ServiceUserEmail);

            if (userId == null)
            {
                var currentUser = await GetCurrentUser();

                if (await HasRole(roleName, currentUser.Id))
                {
                    _logger.LogError("User already has roleName that this method is trying to assign");
                    throw new UnauthorizedException("Your user already has that role");
                }
                else
                {
                    var roleId = _appDbContext.Roles.First(c => c.Name == roleName).Id;

                    await AssertRoleToUserAndSaveItToDatabase(currentUser, serviceUser!, roleId);
                }
            }
            else
            {
                if (await HasRole(roleName, userId))
                {
                    _logger.LogError("User already has roleName that this method is trying to assign");
                    throw new UnauthorizedException("Your user already has that role");
                }
                else
                {
                    var roleId = _appDbContext.Roles.First(c => c.Name == roleName).Id;
                    var currentUser = _appDbContext.Users.Single(c => c.Id == userId);

                    await AssertRoleToUserAndSaveItToDatabase(currentUser, serviceUser!, roleId);
                }
            }
        }

        private async Task<User> GetCurrentUser()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            return currentUser;
        }

        private async Task<List<RoleName>> GetCurrentUserRolesRoleNames()
        {
            var currentUser = await GetCurrentUser();

            var roleNames = currentUser.Roles!
                .Select(c => c.Role!.Name)
                .ToList();

            return roleNames;
        }

        private async Task<List<RoleName>> GetUserRolesRoleNamesFromUserWithId(long? userId)
        {
            var user = await _appDbContext.Users
                .Include(c => c.Roles!).ThenInclude(c => c.Role!)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                _logger.LogError("Failed to get roles for user, because user was not found");
                throw new ApplicationErrorException("Sorry we couldn't find your user in database");
            }

            var roleNames = user.Roles!
                .Select(c => c.Role!.Name)
                .ToList();

            return roleNames;
        }

        private async Task AssertRoleToUserAndSaveItToDatabase(User currentUser, User serviceUser, long roleId)
        {
            var newUserRole = new UserRole
            {
                RoleId = roleId,
                UserId = currentUser.Id,
                UpdateUserId = serviceUser!.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
            _appDbContext.Add(newUserRole);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
