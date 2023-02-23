using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace Donate.Actions.DonateForm.GetWarehouses
{
    public class GetWarehousesAction
    {
        private readonly GetWarehousesService _getWarehousesService;

        public GetWarehousesAction(GetWarehousesService getWarehousesService)
        {
            _getWarehousesService = getWarehousesService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var listOfWarehouses = await _getWarehousesService.GetActiveWarehouses();

            return new OkObjectResult(listOfWarehouses);
        }
    }
}
