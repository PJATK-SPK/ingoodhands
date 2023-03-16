using Autofac;
using Core.Setup.Autofac;

namespace Core.Actions.DonateForm.GetProducts
{
    public class GetProductsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<GetProductsAction>();
            builder.RegisterAsScoped<GetProductsService>();
        }
    }
}
