using FluentValidation;
using Orders.Actions.CreateOrderActions.CreateOrderAddAddress;

namespace Orders.Actions.CreateOrderActions.CreateOrderAddAddress
{
    public class CreateOrderAddAddressPayloadValidator : AbstractValidator<CreateOrderAddAddressPayload>
    {
        public CreateOrderAddAddressPayloadValidator()
        {
            RuleFor(c => c.Id).Length(0, 50);
            RuleFor(c => c.CountryName).NotNull().Length(1, 50);
            RuleFor(c => c.PostalCode).NotNull().Length(1, 10);
            RuleFor(c => c.City).NotNull().Length(1, 50);
            RuleFor(c => c.Street).Length(1, 100);
            RuleFor(c => c.StreetNumber).Length(1, 10);
            RuleFor(c => c.Apartment).Length(1, 10);
            RuleFor(c => c.GpsLatitude).NotNull();
            RuleFor(c => c.GpsLongitude).NotNull();
        }
    }
}