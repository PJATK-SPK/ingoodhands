using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Services;
using Core.Setup.Auth0;
using FluentValidation;

namespace Orders.Actions.CreateOrderActions.CreateOrderCreateOrder
{
    public class CreateOrderFetchService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly RoleService _roleService;
        private readonly CreateOrderCreateOrderPayloadValidator _createOrderCreateOrderPayloadValidator;

        public CreateOrderFetchService(
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            RoleService roleService,
            CreateOrderCreateOrderPayloadValidator createOrderCreateOrderPayloadValidator)
        {
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _roleService = roleService;
            _createOrderCreateOrderPayloadValidator = createOrderCreateOrderPayloadValidator;
        }

        public async Task<User?> GetUser(CreateOrderCreateOrderPayload payload)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            await _roleService.ThrowIfNoRole(RoleName.Needy, currentUser.Id);
            await _createOrderCreateOrderPayloadValidator.ValidateAndThrowAsync(payload);

            return currentUser;
        }
    }
}
