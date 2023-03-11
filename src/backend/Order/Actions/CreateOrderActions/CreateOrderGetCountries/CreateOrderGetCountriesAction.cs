using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.CreateOrderActions.CreateOrderGetCountries
{
    public class CreateOrderGetCountriesAction
    {
        private readonly CreateOrderGetCountriesService _createOrderGetCountriesService;

        public CreateOrderGetCountriesAction(CreateOrderGetCountriesService createOrderGetCountriesService)
        {
            _createOrderGetCountriesService = createOrderGetCountriesService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var result = await _createOrderGetCountriesService.GetActiveCountries();

            return new OkObjectResult(result);
        }
    }
}