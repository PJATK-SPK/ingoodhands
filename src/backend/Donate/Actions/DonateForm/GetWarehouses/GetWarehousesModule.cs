using Autofac;
using Core.Setup.Autofac;

namespace Donate.Actions.DonateForm.GetWarehouses
{
    public class GetWarehousesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<GetWarehousesAction>();
            builder.RegisterAsScoped<GetWarehousesService>();
        }
    }
}
