using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.DeliveriesActions.DeliveriesGetList
{
    public class DeliveriesGetListAction
    {
        private readonly DeliveriesGetListService _deliveriesGetListService;

        public DeliveriesGetListAction(DeliveriesGetListService deliveriesGetListService)
        {
            _deliveriesGetListService = deliveriesGetListService;
        }

        public async Task<OkObjectResult> Execute(int page, int pageSize, string? filter = null)
        {
            var result = await _deliveriesGetListService.GetList(page, pageSize, filter);

            return new OkObjectResult(result);
        }
    }
}
