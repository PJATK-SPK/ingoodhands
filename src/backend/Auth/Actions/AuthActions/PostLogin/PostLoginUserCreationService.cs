using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Exceptions;
using Core.Setup.Auth0;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Auth.Actions.AuthActions.PostLogin
{
    public class PostLoginUserCreationService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<PostLoginUserCreationService> _logger;

        public PostLoginUserCreationService(AppDbContext appDbContext, ILogger<PostLoginUserCreationService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<User> CreateUserAndAddToDatabase(CurrentUserInfo auth0UserInfo)
        {
            var user = await _appDbContext.Users.Include(c => c.Roles).SingleOrDefaultAsync(c => c.Email == auth0UserInfo.Email);
            var auth0UserFromDatabase = await _appDbContext.Auth0Users.SingleOrDefaultAsync(c => c.Identifier == auth0UserInfo.Identifier);

            var serviceUser = await _appDbContext.Users.SingleOrDefaultAsync(c => c.Email == DbConstants.ServiceUserEmail);
            if (serviceUser == null)
            {
                _logger.LogError("Service user was not found in database!");
                throw new ApplicationErrorException("Sorry there seems to be a problem with our service");
            }

            if (user == null && auth0UserFromDatabase == null)
            {
                user = CreateUser(auth0UserInfo);
                _appDbContext.Add(user);

                var userRoles = await CreateUserRoles(user, serviceUser);
                _appDbContext.AddRange(userRoles);

                var newAuth0User = CreateAuth0User(auth0UserInfo, user, serviceUser!);
                _appDbContext.Add(newAuth0User);

            }
            else if (user != null && auth0UserFromDatabase == null)
            {
                var newAuth0User = CreateAuth0User(auth0UserInfo, user, serviceUser!);
                _appDbContext.Add(newAuth0User);

                var userRoles = await CreateUserRoles(user, serviceUser);
                _appDbContext.AddRange(userRoles);
            }
            else if (user == null && auth0UserFromDatabase != null)
            {
                user = CreateUser(auth0UserInfo);
                _appDbContext.Add(user);

                var userRoles = await CreateUserRoles(user, serviceUser);
                _appDbContext.AddRange(userRoles);
            }

            if (!user!.Roles!.Any())
            {
                var userRoles = await CreateUserRoles(user, serviceUser);
                _appDbContext.AddRange(userRoles);
            }

            _appDbContext.SaveChanges();

            return user!;
        }
        private static User CreateUser(CurrentUserInfo currentAuth0UserInfo)
        {
            var firstName = string.IsNullOrWhiteSpace(currentAuth0UserInfo.GivenName)
                ? string.IsNullOrWhiteSpace(currentAuth0UserInfo.Name!)
                    ? "User"
                    : currentAuth0UserInfo.Name!
                : currentAuth0UserInfo.GivenName!;

            return new User
            {
                Status = DbEntityStatus.Active,
                FirstName = firstName,
                LastName = currentAuth0UserInfo.FamilyName,
                Email = currentAuth0UserInfo.Email!,
                WarehouseId = null
            };
        }

        private static Auth0User CreateAuth0User(CurrentUserInfo currentAuth0UserInfo, User user, User serviceUser)
        {
            return new Auth0User
            {
                FirstName = currentAuth0UserInfo.GivenName,
                LastName = currentAuth0UserInfo.FamilyName,
                Nickname = currentAuth0UserInfo.Nickname,
                Name = currentAuth0UserInfo.Name,
                UpdateUserId = serviceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Email = currentAuth0UserInfo.Email!,
                Identifier = currentAuth0UserInfo.Identifier!,
                User = user
            };
        }

        private async Task<List<UserRole>> CreateUserRoles(User user, User serviceUser)
        {
            var roles = await _appDbContext.Roles.ToListAsync();

            var donorRole = roles.SingleOrDefault(c => c.Name == RoleName.Donor);
            var needyRole = roles.SingleOrDefault(c => c.Name == RoleName.Needy);

            if (donorRole == null || needyRole == null)
            {
                _logger.LogError("There is no needy and donor roles in database!");
                throw new ApplicationErrorException("There is something wrong with the application");
            }

            var donor = CreateUserRole(user, serviceUser);
            donor.RoleId = donorRole.Id;

            var needy = CreateUserRole(user, serviceUser);
            needy.RoleId = needyRole.Id;

            return new List<UserRole> { donor, needy };
        }

        private static UserRole CreateUserRole(User user, User serviceUser)
        {
            return new UserRole
            {
                User = user,
                UpdateUserId = serviceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
        }
    }
}
