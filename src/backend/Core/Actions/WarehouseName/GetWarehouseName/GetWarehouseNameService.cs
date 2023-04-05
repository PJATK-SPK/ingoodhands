using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Actions.WarehouseName.GetWarehouseName
{
    public class GetWarehouseNameService
    {
        private readonly ILogger<GetWarehouseNameService> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly RoleService _roleService;

        public GetWarehouseNameService(ILogger<GetWarehouseNameService> logger, ICurrentUserService currentUserService, GetCurrentUserService getCurrentUserService, RoleService roleService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _roleService = roleService;
        }

        public async Task<GetWarehouseNameResponse> GetUserWarehouseName()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            await _roleService.ThrowIfNoRole(RoleName.WarehouseKeeper, currentUser.Id);

            var usersWarehouse = currentUser.Warehouse?.ShortName;

            if (usersWarehouse == null)
            {
                _logger.LogError("Couldn't find warehouse name for user with id:{currentUserId}", currentUser.Id);
                throw new ItemNotFoundException("Sorry we couldn't find that warehouse name assigned to this user");
            }

            var response = new GetWarehouseNameResponse
            {
                WarehouseName = usersWarehouse,
            };

            return response;
        }
    }
}
