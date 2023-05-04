using Microsoft.AspNetCore.Mvc;
using Orders.Actions.CreateOrderActions.CreateOrderCreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesHasActiveDelivery
{
    public class AvailableDeliveriesHasActiveDeliveryAction
    {
        private readonly AvailableDeliveriesHasActiveDeliveryService _availableDeliveriesHasActiveDeliveryService;

        public AvailableDeliveriesHasActiveDeliveryAction(AvailableDeliveriesHasActiveDeliveryService availableDeliveriesHasActiveDeliveryService)
        {
            _availableDeliveriesHasActiveDeliveryService = availableDeliveriesHasActiveDeliveryService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var result = await _availableDeliveriesHasActiveDeliveryService.HasActiveDelivery();

            return new OkObjectResult(result);
        }
    }
}
