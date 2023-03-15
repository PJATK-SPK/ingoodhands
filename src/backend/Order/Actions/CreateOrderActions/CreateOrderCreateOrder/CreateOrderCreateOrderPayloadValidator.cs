using FluentValidation;

namespace Orders.Actions.CreateOrderActions.CreateOrderCreateOrder
{
    public class CreateOrderCreateOrderPayloadValidator : AbstractValidator<CreateOrderCreateOrderPayload>
    {
        public CreateOrderCreateOrderPayloadValidator()
        {
            RuleFor(c => c.AddressId).NotNull().Length(1, 50);
            RuleForEach(c => c.Products)
                .ChildRules(products =>
                {
                    products.RuleFor(p => p.Id).NotNull().Length(1, 50);
                    products.RuleFor(p => p.Quantity).InclusiveBetween(1, 1000);
                });
        }
    }
}
