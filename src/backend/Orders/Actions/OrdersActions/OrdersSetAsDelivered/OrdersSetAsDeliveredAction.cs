using Core.Database.Enums;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.OrdersActions.OrdersSetAsDelivered
{
    public class OrdersSetAsDeliveredAction
    {
        private readonly OrdersSetAsDeliveredService _service;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly RoleService _roleService;
        private readonly Hashids _hashids;

        public OrdersSetAsDeliveredAction(
            OrdersSetAsDeliveredService service,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            RoleService roleService,
            Hashids hashids)
        {
            _service = service;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _roleService = roleService;
            _hashids = hashids;
        }

        public async Task<OkObjectResult> Execute(string orderId, string deliveryId)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = _getCurrentUserService.Execute(auth0UserInfo).Result;
            await _roleService.ThrowIfNoRole(RoleName.Needy, currentUser.Id);

            var decodedOrderId = _hashids.DecodeSingleLong(orderId);
            var decodedDeliveryId = _hashids.DecodeSingleLong(deliveryId);

            await _service.SetAsDelivered(decodedOrderId, decodedDeliveryId, currentUser!);

            return new OkObjectResult(new { Message = "OK" });
        }
    }
}
