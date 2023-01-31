using AuthService.BusinessLogic.GetAuth0UsersByCurrentUser;
using Core.Auth0;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic.PatchUserDetails
{
    public class PatchUserDetailsPayloadDataValidationService
    {
        private readonly ILogger<PatchUserDetailsPayloadDataValidationService> _logger;

        public PatchUserDetailsPayloadDataValidationService(ILogger<PatchUserDetailsPayloadDataValidationService> logger)
        {
            _logger = logger;
        }

        public void Check(PatchUserDetailsPayload userDetailsPayload)
        {
            var isValid = (userDetailsPayload.FirstName != null && userDetailsPayload.FirstName.Length <= 50);
            isValid &= (userDetailsPayload.LastName != null && userDetailsPayload.LastName.Length <= 50);

            if (!isValid)
            {
                _logger.LogError("UserSettingsPayload in PatchUserDetailsPayloadDataValidationService is didn't pass validation (null or too long)");
                throw new ArgumentNullException(null, "Sorry we couldn't save your data. Please contact server administrator.");
            }
        }
    }
}
