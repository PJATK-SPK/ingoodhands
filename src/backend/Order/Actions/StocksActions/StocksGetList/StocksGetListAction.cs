using Microsoft.AspNetCore.Mvc;

namespace Orders.Actions.StocksActions.StocksGetList
{
    public class StocksGetListAction
    {
        private readonly StocksGetListService _stocksGetListService;

        public StocksGetListAction(StocksGetListService stocksGetListService)
        {
            _stocksGetListService = stocksGetListService;
        }

        public async Task<ActionResult> Execute(int page, int pageSize)
        {
            var response = await _stocksGetListService.GetStockList(page, pageSize);

            return new OkObjectResult(response.Value);
        }
    }
}
