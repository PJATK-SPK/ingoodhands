using Core.Setup.Auth0;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Actions.DonateForm.PerformDonate
{
    public class PerformDonateAction
    {
        private readonly PerformDonateService _performDonateService;

        public PerformDonateAction(PerformDonateService performDonateService)
        {
            _performDonateService = performDonateService;
        }

        public async Task<OkObjectResult> Execute(PerformDonatePayload payload)
        {
            var result = await _performDonateService.PerformDonation(payload);

            return new OkObjectResult(result);
        }
    }
}
