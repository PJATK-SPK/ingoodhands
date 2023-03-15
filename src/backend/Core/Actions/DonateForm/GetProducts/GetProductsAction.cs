using Microsoft.AspNetCore.Mvc;

namespace Core.Actions.DonateForm.GetProducts
{
    public class GetProductsAction
    {
        private readonly GetProductsService _getProductsService;

        public GetProductsAction(GetProductsService getProductsService)
        {
            _getProductsService = getProductsService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var listOfProducts = await _getProductsService.GetProducts();

            return new OkObjectResult(listOfProducts);
        }
    }
}
