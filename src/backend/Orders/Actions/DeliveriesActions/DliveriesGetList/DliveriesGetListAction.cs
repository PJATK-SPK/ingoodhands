using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Actions.DeliveriesActions.DliveriesGetList
{
    public class DliveriesGetListAction
    {
        private readonly DliveriesGetListService _dliveriesGetListService;

        public DliveriesGetListAction(DliveriesGetListService dliveriesGetListService)
        {
            _dliveriesGetListService = dliveriesGetListService;
        }

        public async Task<OkObjectResult> Execute(int page, int pageSize, string? filter = null)
        {
            var result = await _dliveriesGetListService.GetList(page, pageSize, filter);

            return new OkObjectResult(result);
        }
    }
}
