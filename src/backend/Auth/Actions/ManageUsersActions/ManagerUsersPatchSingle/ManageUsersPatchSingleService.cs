using Auth.Actions.ManageUsersActions.ManageUsersPatchSingle;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Exceptions;
using Core.Services;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Actions.ManageUsersActions.ManagerUsersPatchSingle
{
    public class ManageUsersPatchSingleService
    {
        private readonly AppDbContext _appDbContext;
        private readonly RoleService _roleService;
        private readonly Hashids _hashids;
        private readonly ILogger<ManageUsersPatchSingleService> _logger;

        public ManageUsersPatchSingleService(
            AppDbContext appDbContext,
            RoleService roleService,
            Hashids hashids,
            ILogger<ManageUsersPatchSingleService> logger
            )
        {
            _appDbContext = appDbContext;
            _roleService = roleService;
            _hashids = hashids;
            _logger = logger;
        }

        public async Task<ManageUsersPatchSingleResponseItem> PatchUserRolesAndWarehouseId(string id, ManageUsersPatchSinglePayload payload)
        {
            await _roleService.ThrowIfNoRole(RoleName.Administrator);
            var userId = _hashids.DecodeSingleLong(id);

            var dbResult = await _appDbContext.Users
                 .Include(c => c.Roles)!
                     .ThenInclude(c => c.Role)
                 .Include(c => c.Warehouse)
                 .SingleOrDefaultAsync(c => c.Id == userId);

            if (dbResult == null)
            {
                _logger.LogError("Couldn't find user by id in database");
                throw new ItemNotFoundException("Sorry we are unable to find this user in database");
            }

            if (!payload.Roles.Any())
            {
                _logger.LogError("There were no roles in the payload");
                throw new ClientInputErrorException("Sorry we couldn't fetch roles");
            }

            var roleNamesToAdd = payload.Roles.Where(c => !dbResult.Roles!.Select(c => c.Role!.Name).Contains(Enum.Parse<RoleName>(c)));
            var rolesToRemove = dbResult.Roles!.Where(c => !payload.Roles.Contains(c.Role!.Name.ToString())).ToList();

            foreach (var role in rolesToRemove)
            {
                dbResult.Roles!.Remove(role);
            }

            var rolesToAdd = await _appDbContext.Roles
                .ToListAsync(); // materialize the query

            rolesToAdd = rolesToAdd
                .Where(r => roleNamesToAdd.Contains(r.Name.ToString())) // perform the Contains locally
                .ToList();
            foreach (var role in rolesToAdd)
            {
                if (role != null)
                {
                    var userRole = new UserRole
                    {
                        UserId = dbResult.Id,
                        RoleId = role.Id,
                        UpdatedAt = DateTime.UtcNow,
                        UpdateUserId = dbResult.Id,
                        Status = DbEntityStatus.Active
                    };
                    dbResult.Roles!.Add(userRole);
                }
            }

            if (payload.WarehouseId != null)
            {
                var warehouseId = _hashids.DecodeSingleLong(payload.WarehouseId);
                dbResult.Warehouse = null;
                dbResult.WarehouseId = warehouseId;
            }
            else
            {
                dbResult.WarehouseId = null;
            }

            await _appDbContext.SaveChangesAsync();

            var response = new ManageUsersPatchSingleResponseItem
            {
                Id = _hashids.EncodeLong(userId),
                FullName = dbResult.FirstName + " " + dbResult.LastName,
                WarehouseId = dbResult.WarehouseId == null ? null : _hashids.EncodeLong((long)dbResult.WarehouseId),
                Roles = dbResult.Roles!.Select(c => c.Role!.Name.ToString()).ToList()
            };

            return response;
        }
    }
}
