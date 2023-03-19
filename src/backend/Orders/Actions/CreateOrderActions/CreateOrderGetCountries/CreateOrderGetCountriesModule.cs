using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.CreateOrderActions.CreateOrderGetCountries
{
    public class CreateOrderGetCountriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<CreateOrderGetCountriesAction>();
            builder.RegisterAsScoped<CreateOrderGetCountriesService>();
        }
    }
}
