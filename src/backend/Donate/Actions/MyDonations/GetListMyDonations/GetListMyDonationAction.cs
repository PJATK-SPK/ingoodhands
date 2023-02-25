using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using Donate.Actions.DonateForm.GetWarehouses;
using Donate.Actions.DonateForm.PerformDonate;
using Donate.Services.DonateNameBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Actions.MyDonations.GetList
{
    public class GetListMyDonationAction
    {
        private readonly GetListMyDonationService _getListMyDonationService;

        public GetListMyDonationAction(GetListMyDonationService getListMyDonationService)
        {
            _getListMyDonationService = getListMyDonationService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var listOfMyDonations = await _getListMyDonationService.GetListMyDonations();

            return new OkObjectResult(listOfMyDonations);
        }
    }
}
