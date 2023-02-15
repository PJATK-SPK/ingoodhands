using Auth.Models;
using Core.Exceptions;
using FluentValidation;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Actions.UserSettingsActions.PatchUserDetails
{
    public class PatchUserDetailsAction
    {
        private readonly PatchUserDetailsService _patchUserDetailsService;
        private readonly PatchUserDetailsPayloadValidator _patchUserDetailsPayloadValidator;
        private readonly Hashids _hashids;

        public PatchUserDetailsAction(
            PatchUserDetailsService updateUserDetailsService,
            PatchUserDetailsPayloadValidator patchUserDetailsPayloadDataValidationService,
            Hashids hashids)
        {
            _patchUserDetailsService = updateUserDetailsService;
            _patchUserDetailsPayloadValidator = patchUserDetailsPayloadDataValidationService;
            _hashids = hashids;
        }

        public async Task<OkObjectResult> Execute(PatchUserDetailsPayload userSettingsPayload, string hashid)
        {
            var id = _hashids.DecodeSingleLong(hashid);
            _patchUserDetailsPayloadValidator.ValidateAndThrow(userSettingsPayload);

            var patchedUser = await _patchUserDetailsService.PatchUserFirstNameAndLastName(userSettingsPayload, id);

            var result = new UserDetailsResponse(patchedUser, _hashids);

            return new OkObjectResult(result);
        }
    }
}