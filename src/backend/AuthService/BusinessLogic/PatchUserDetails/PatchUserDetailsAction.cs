using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.BusinessLogic.PatchUserDetails
{
    public class PatchUserDetailsAction
    {
        private readonly PatchUserDetailsService _patchUserDetailsService;
        private readonly PatchUserDetailsPayloadValidator _patchUserDetailsPayloadValidator;

        public PatchUserDetailsAction(

            PatchUserDetailsService updateUserDetailsService,
            PatchUserDetailsPayloadValidator patchUserDetailsPayloadDataValidationService
            )
        {
            _patchUserDetailsService = updateUserDetailsService;
            _patchUserDetailsPayloadValidator = patchUserDetailsPayloadDataValidationService;
        }

        public async Task<OkObjectResult> Execute(PatchUserDetailsPayload userSettingsPayload, long id)
        {
            var validation = _patchUserDetailsPayloadValidator.Validate(userSettingsPayload);
            if (!validation.IsValid) throw new HttpError400Exception(validation.ToString());

            var patchedUser = await _patchUserDetailsService.PatchUserFirstNameAndLastName(userSettingsPayload, id);

            return new OkObjectResult(patchedUser);
        }
    }
}