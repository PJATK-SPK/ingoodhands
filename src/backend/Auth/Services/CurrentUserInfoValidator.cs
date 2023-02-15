using Core.Setup.Auth0;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Auth.Services
{
    public class CurrentUserInfoValidator : AbstractValidator<CurrentUserInfo>
    {
        public CurrentUserInfoValidator(ILogger<CurrentUserInfoValidator> logger)
        {
            RuleFor(c => c.FamilyName).NotNull().MaximumLength(50);
            RuleFor(c => c.GivenName).NotNull().MaximumLength(50);
            RuleFor(c => c.Locale).NotNull().MaximumLength(10);
            RuleFor(c => c.Name).NotNull().MaximumLength(50);
            RuleFor(c => c.Nickname).NotNull().MaximumLength(50);
            RuleFor(c => c.Identifier).NotNull().MaximumLength(80);
            RuleFor(c => c.Email).NotNull().EmailAddress().MaximumLength(254);
            RuleFor(c => c.EmailVerified).Equal(true).WithMessage("Please confirm you address email via Auth0!");
        }
    }
}
