using AuthService.BusinessLogic.GetAuth0UsersByCurrentUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthService.BusinessLogic.PatchUserDetails
{
    public class PatchUserDetailsAction
    {
        private readonly ILogger<GetAuth0UsersByCurrentUserAction> _logger;
        private readonly PatchUserDetailsService _patchUserDetailsService;
        private readonly PatchUserDetailsPayloadDataValidationService _patchUserDetailsPayloadDataValidationService;

        public PatchUserDetailsAction(
            ILogger<GetAuth0UsersByCurrentUserAction> logger,
            PatchUserDetailsService updateUserDetailsService,
            PatchUserDetailsPayloadDataValidationService patchUserDetailsPayloadDataValidationService
            )
        {
            _logger = logger;
            _patchUserDetailsService = updateUserDetailsService;
            _patchUserDetailsPayloadDataValidationService = patchUserDetailsPayloadDataValidationService;
        }

        public async Task<ObjectResult> Execute(PatchUserDetailsPayload userSettingsPayload, long id)
        {
            var patchUserDetailsPayloadIsValid = _patchUserDetailsPayloadDataValidationService.Check(userSettingsPayload);

            if (!patchUserDetailsPayloadIsValid)
            {
                _logger.LogError("UserSettingsPayload in PatchUserDetails is null");
                throw new ArgumentNullException(null, "Sorry we couldn't save you data. Please contact server administrator.");
            }

            var isUserDataPatched = await _patchUserDetailsService.PatchUserFirstNameAndLastName(userSettingsPayload, id);

            return new ObjectResult(isUserDataPatched);
        }
    }
}