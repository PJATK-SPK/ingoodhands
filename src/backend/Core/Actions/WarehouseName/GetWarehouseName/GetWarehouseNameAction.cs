using Microsoft.AspNetCore.Mvc;

namespace Core.Actions.WarehouseName.GetWarehouseName
{
    public class GetWarehouseNameAction
    {
        private readonly GetWarehouseNameService _getWarehouseNameService;

        public GetWarehouseNameAction(GetWarehouseNameService getWarehouseNameService)
        {
            _getWarehouseNameService = getWarehouseNameService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var result = await _getWarehouseNameService.GetUserWarehouseName();

            return new OkObjectResult(result);
        }
    }
}
