using AuthService.BusinessLogic.GetAuth0UsersByCurrentUser;
using Core.Auth0;
using Core.Exceptions;
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
            var isValid = userDetailsPayload.FirstName != null;
            isValid &= userDetailsPayload.LastName != null;

            if (!isValid)
            {
                _logger.LogError("UserDetailsPayload in PatchUserDetailsPayloadDataValidationService didn't pass null validation");
                throw new HttpError400Exception("Sorry, You cannot save empty field");
            }

            var isValidLength = userDetailsPayload.FirstName.Length <= 50;
            isValidLength &= userDetailsPayload.LastName.Length <= 50;

            if (!isValidLength)
            {
                _logger.LogError("UserDetailsPayload in PatchUserDetailsPayloadDataValidationService didn't pass length validation");
                throw new HttpError400Exception("Sorry, You cannot save name longer than 50 characters");
            }
        }
    }
}
