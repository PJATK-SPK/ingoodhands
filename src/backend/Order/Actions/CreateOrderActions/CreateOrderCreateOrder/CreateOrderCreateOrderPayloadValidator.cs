using FluentValidation;

namespace Orders.Actions.CreateOrderActions.CreateOrderCreateOrder
{
    public class CreateOrderCreateOrderPayloadValidator : AbstractValidator<CreateOrderCreateOrderPayload>
    {
        public CreateOrderCreateOrderPayloadValidator()
        {
            RuleFor(c => c.AddressId).NotNull().Length(1, 50);
        }
    }
}
