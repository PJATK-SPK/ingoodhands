using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.StocksActions.StocksGetList
{
    public class StocksGetListModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<StocksGetListAction>();
        }
    }
}
