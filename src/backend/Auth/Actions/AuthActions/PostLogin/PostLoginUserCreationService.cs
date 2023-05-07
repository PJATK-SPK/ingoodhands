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
            var user = await _appDbContext.Users.Include(c => c.Roles)!.ThenInclude(c => c.Role).SingleOrDefaultAsync(c => c.Email == auth0UserInfo.Email);
            var auth0UserFromDatabase = await _appDbContext.Auth0Users.SingleOrDefaultAsync(c => c.Identifier == auth0UserInfo.Identifier);

            var serviceUser = await _appDbContext.Users.SingleOrDefaultAsync(c => c.Email == DbConstants.ServiceUserEmail);
            if (serviceUser == null)
            {
                _logger.LogError("Service user was not found in database!");
                throw new ApplicationErrorException("Sorry there seems to be a problem with our service");
            }

            if (user != null && auth0UserFromDatabase != null)
            {
                UpdateAuth0User(auth0UserFromDatabase, auth0UserInfo);
            }
            else if (user == null && auth0UserFromDatabase == null)
            {
                user = CreateUser(auth0UserInfo);
                await _appDbContext.AddAsync(user);

                await CreateUserRoles(user, serviceUser);

                var newAuth0User = CreateAuth0User(auth0UserInfo, user, serviceUser!);
                await _appDbContext.AddAsync(newAuth0User);

            }
            else if (user != null && auth0UserFromDatabase == null)
            {
                var newAuth0User = CreateAuth0User(auth0UserInfo, user, serviceUser!);
                await _appDbContext.AddAsync(newAuth0User);

                await CreateUserRoles(user, serviceUser);
            }

            if (!user!.Roles!.Any())
            {
                await CreateUserRoles(user, serviceUser);
            }

            await _appDbContext.SaveChangesAsync();

            return user!;
        }
        private static User CreateUser(CurrentUserInfo currentAuth0UserInfo)
        {
            return new User
            {
                Status = DbEntityStatus.Active,
                FirstName = GetFirstName(currentAuth0UserInfo),
                LastName = currentAuth0UserInfo.FamilyName,
                Email = currentAuth0UserInfo.Email!,
                WarehouseId = null,
                Roles = new List<UserRole>()
            };
        }

        private static string GetFirstName(CurrentUserInfo currentAuth0UserInfo)
        {
            var firstName = "User";

            if (!string.IsNullOrWhiteSpace(currentAuth0UserInfo.GivenName))
                firstName = currentAuth0UserInfo.GivenName!.Trim();
            else if (!string.IsNullOrWhiteSpace(currentAuth0UserInfo.Name!))
                firstName = currentAuth0UserInfo.Name!.Trim();

            return firstName;
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
                PictureURL = currentAuth0UserInfo.PictureURL!,
                Identifier = currentAuth0UserInfo.Identifier!,
                User = user
            };
        }

        private static void UpdateAuth0User(Auth0User auth0User, CurrentUserInfo currentAuth0UserInfo)
        {
            auth0User.FirstName = currentAuth0UserInfo.GivenName;
            auth0User.LastName = currentAuth0UserInfo.FamilyName;
            auth0User.Nickname = currentAuth0UserInfo.Nickname;
            auth0User.Name = currentAuth0UserInfo.Name;
            auth0User.PictureURL = currentAuth0UserInfo.PictureURL;
        }

        private async Task CreateUserRoles(User user, User serviceUser)
        {
            var roles = await _appDbContext.Roles.ToListAsync();

            var donorRole = roles.SingleOrDefault(c => c.Name == RoleName.Donor);
            var needyRole = roles.SingleOrDefault(c => c.Name == RoleName.Needy);

            if (donorRole == null || needyRole == null)
            {
                _logger.LogError("There is no needy and donor roles in database!");
                throw new ApplicationErrorException("There is something wrong with the application");
            }

            if (!user.Roles!.Select(c => c.Role!.Name).Contains(RoleName.Donor))
            {
                var donor = CreateUserRole(user, serviceUser, donorRole);
                _appDbContext.UserRoles.Add(donor);
            }

            if (!user.Roles!.Select(c => c.Role!.Name).Contains(RoleName.Needy))
            {
                var needy = CreateUserRole(user, serviceUser, needyRole);
                _appDbContext.UserRoles.Add(needy);
            }
        }

        private static UserRole CreateUserRole(User user, User serviceUser, Role role)
        {
            return new UserRole
            {
                User = user,
                UpdateUserId = serviceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active,
                Role = role,
                RoleId = role.Id
            };
        }
    }
}
