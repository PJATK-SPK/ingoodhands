using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.CreateOrderActions.CreateOrderDeleteAddress
{
    public class CreateOrderDeleteAddressAction
    {
        private readonly CreateOrderDeleteAddressService _createOrderDeleteAddressService;

        public CreateOrderDeleteAddressAction(CreateOrderDeleteAddressService createOrderDeleteAddressService)
        {
            _createOrderDeleteAddressService = createOrderDeleteAddressService;
        }

        public async Task<OkObjectResult> Execute(string id)
        {
            var result = await _createOrderDeleteAddressService.DeleteAddressById(id);

            return new OkObjectResult(result);
        }
    }
}
