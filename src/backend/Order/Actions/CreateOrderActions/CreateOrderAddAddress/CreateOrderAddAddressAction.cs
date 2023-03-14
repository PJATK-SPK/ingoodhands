using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.CreateOrderActions.CreateOrderAddAddress
{
    public class CreateOrderAddAddressAction
    {
        private readonly CreateOrderAddAddressService _createOrderAddAddressService;

        public CreateOrderAddAddressAction(CreateOrderAddAddressService createOrderAddAddressService)
        {
            _createOrderAddAddressService = createOrderAddAddressService;
        }

        public async Task<OkObjectResult> Execute(CreateOrderAddAddressPayload payload)
        {
            var result = await _createOrderAddAddressService.AddAddress(payload);

            return new OkObjectResult(result);
        }
    }
}
