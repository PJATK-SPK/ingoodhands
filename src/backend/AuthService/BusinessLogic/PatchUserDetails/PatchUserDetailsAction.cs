using AuthService.BusinessLogic.GetAuth0UsersByCurrentUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthService.BusinessLogic.PatchUserDetails
{
    public class PatchUserDetailsAction
    {
        private readonly PatchUserDetailsService _patchUserDetailsService;
        private readonly PatchUserDetailsPayloadDataValidationService _patchUserDetailsPayloadDataValidationService;

        public PatchUserDetailsAction(

            PatchUserDetailsService updateUserDetailsService,
            PatchUserDetailsPayloadDataValidationService patchUserDetailsPayloadDataValidationService
            )
        {
            _patchUserDetailsService = updateUserDetailsService;
            _patchUserDetailsPayloadDataValidationService = patchUserDetailsPayloadDataValidationService;
        }

        public async Task<OkObjectResult> Execute(PatchUserDetailsPayload userSettingsPayload, long id)
        {
            _patchUserDetailsPayloadDataValidationService.Check(userSettingsPayload);

            var patchedUser = await _patchUserDetailsService.PatchUserFirstNameAndLastName(userSettingsPayload, id);

            return new OkObjectResult(patchedUser);
        }
    }
}