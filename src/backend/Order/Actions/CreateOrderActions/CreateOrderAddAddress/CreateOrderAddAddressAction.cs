using Microsoft.AspNetCore.Mvc;
using Orders.Actions.CreateOrderActions.Shared;
using Orders.Actions.CreateOrderActions.CreateOrderGetCountries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Actions.CreateOrderActions.CreateOrderAddAddresses
{
    public class CreateOrderAddAddressAction
    {
        private readonly CreateOrderAddAddressService _createOrderAddAddressService;

        public CreateOrderAddAddressAction(CreateOrderAddAddressService createOrderAddAddressService)
        {
            _createOrderAddAddressService = createOrderAddAddressService;
        }

        public async Task<OkObjectResult> Execute(CreateOrderAddAddressPayload payload)
        {
            var result = await _createOrderAddAddressService.AddAddress(payload);

            return new OkObjectResult(result);
        }
    }
}
