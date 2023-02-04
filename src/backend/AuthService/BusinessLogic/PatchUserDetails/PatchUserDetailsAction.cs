using Core.Exceptions;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.BusinessLogic.PatchUserDetails
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
            var validation = _patchUserDetailsPayloadValidator.Validate(userSettingsPayload);
            if (!validation.IsValid) throw new HttpError400Exception(validation.ToString());

            var patchedUser = await _patchUserDetailsService.PatchUserFirstNameAndLastName(userSettingsPayload, id);

            var result = new PatchUserDetailsResponse
            {
                Id = _hashids.EncodeLong(patchedUser.Id),
                FirstName = patchedUser.FirstName,
                LastName = patchedUser.LastName,
            };

            return new OkObjectResult(result);
        }
    }
}