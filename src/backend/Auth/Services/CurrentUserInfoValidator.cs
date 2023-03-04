using Core.Setup.Auth0;
using FluentValidation;

namespace Auth.Services
{
    public class CurrentUserInfoValidator : AbstractValidator<CurrentUserInfo>
    {
        public CurrentUserInfoValidator()
        {
            RuleFor(c => c.FamilyName).MaximumLength(50);
            RuleFor(c => c.GivenName).MaximumLength(50);
            RuleFor(c => c.Locale).MaximumLength(10);
            RuleFor(c => c.Name).MaximumLength(50);
            RuleFor(c => c.Nickname).MaximumLength(50);
            RuleFor(c => c.Identifier).NotNull().MaximumLength(80);
            RuleFor(c => c.Email).NotNull().EmailAddress().MaximumLength(254);
            RuleFor(c => c.EmailVerified).Equal(true).WithMessage("Please confirm you address email via Auth0!");
        }
    }
}
