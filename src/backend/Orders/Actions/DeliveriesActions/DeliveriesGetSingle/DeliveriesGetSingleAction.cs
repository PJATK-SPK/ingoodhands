using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.DeliveriesActions.DeliveriesGetSingle
{
    public class DeliveriesGetSingleAction
    {
        private readonly DeliveriesGetSingleService _deliveriesGetSingleService;

        public DeliveriesGetSingleAction(DeliveriesGetSingleService deliveriesGetSingleService)
        {
            _deliveriesGetSingleService = deliveriesGetSingleService;
        }

        public async Task<OkObjectResult> Execute(string id)
        {
            var result = await _deliveriesGetSingleService.GetSingle(id);

            return new OkObjectResult(result);
        }
    }
}
