﻿using FluentValidation;

namespace AuthService.BusinessLogic.PatchUserDetails
{
    public class PatchUserDetailsPayloadValidator : AbstractValidator<PatchUserDetailsPayload>
    {
        public PatchUserDetailsPayloadValidator()
        {
            RuleFor(c => c.FirstName).NotNull().Length(1, 50);
            RuleFor(c => c.LastName).NotNull().Length(1, 50);
        }
    }
}
