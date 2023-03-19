using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.CreateOrderActions.CreateOrderGetAddresses
{
    public class CreateOrderGetAddressesAction
    {
        private readonly CreateOrderGetAddressesService _createOrderGetAddressesService;

        public CreateOrderGetAddressesAction(CreateOrderGetAddressesService createOrderGetAddressesService)
        {
            _createOrderGetAddressesService = createOrderGetAddressesService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var result = await _createOrderGetAddressesService.GetActiveAddresses();

            return new OkObjectResult(result);
        }
    }
}
