using Donate.Actions.MyDonations.GetList;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Actions.MyDonations.GetDetails
{
    public class GetMyDonationDetailsAction
    {
        private readonly GetMyDonationDetailsService _getMyDonationDetailsService;

        public GetMyDonationDetailsAction(GetMyDonationDetailsService getMyDonationDetailsService)
        {
            _getMyDonationDetailsService = getMyDonationDetailsService;
        }

        public async Task<OkObjectResult> Execute(string id)
        {
            var result = await _getMyDonationDetailsService.GetMyDonationDetails(id);

            return new OkObjectResult(result);
        }
    }
}
