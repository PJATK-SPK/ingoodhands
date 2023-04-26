using Core.Database;
using Core.Database.Enums;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Services;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Auth.Actions.ManageUsersActions.ManageUsersGetSingle
{
    public class ManageUsersGetSingleAction
    {
        private readonly AppDbContext _appDbContext;
        private readonly RoleService _roleService;
        private readonly Hashids _hashids;
        private readonly ILogger<ManageUsersGetSingleAction> _logger;

        public ManageUsersGetSingleAction(
            AppDbContext appDbContext,
            RoleService roleService,
            Hashids hashids,
            ILogger<ManageUsersGetSingleAction> logger
            )
        {
            _appDbContext = appDbContext;
            _roleService = roleService;
            _hashids = hashids;
            _logger = logger;
        }

        public async Task<OkObjectResult> Execute(string id)
        {
            await _roleService.ThrowIfNoRole(RoleName.Administrator);
            var userId = _hashids.DecodeSingleLong(id);

            var dbResult = await _appDbContext.Users
                 .Include(c => c.Auth0Users)
                 .Include(c => c.Roles)!
                     .ThenInclude(c => c.Role)
                 .Include(c => c.Warehouse)
                 .SingleOrDefaultAsync(c => c.Id == userId);

            if (dbResult == null)
            {
                _logger.LogError("Couldn't find user with ID:{userId} in database", userId);
                throw new ItemNotFoundException("Sorry we are unable to find this user in database");
            }

            var response = new ManageUsersGetSingleResponseItem
            {
                Id = _hashids.EncodeLong(userId),
                FullName = dbResult.FirstName + " " + dbResult.LastName,
                Email = dbResult.Email,
                PictureUrl = dbResult.Auth0Users!.First().PictureURL,
                WarehouseId = dbResult.WarehouseId != null ? _hashids.EncodeLong(dbResult.WarehouseId!.Value) : null,
                Roles = dbResult.Roles!.Select(c => c.Role!.Name.ToString()).ToList()
            };

            return new OkObjectResult(response);
        }
    }
}
