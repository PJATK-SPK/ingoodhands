using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesGetList
{
    public class AvailableDeliveriesGetListAction
    {
        private readonly AvailableDeliveriesGetListService _availableDeliveriesGetListService;

        public AvailableDeliveriesGetListAction(AvailableDeliveriesGetListService availableDeliveriesGetListService)
        {
            _availableDeliveriesGetListService = availableDeliveriesGetListService;
        }

        public async Task<OkObjectResult> Execute(int page, int pageSize, string? filter = null)
        {
            var result = await _availableDeliveriesGetListService.GetList(page, pageSize, filter);

            return new OkObjectResult(result);
        }
    }
}
