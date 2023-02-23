using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Actions.DonateForm.GetProducts
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
