using Autofac;
using Core.Setup.Autofac;

namespace Order.Actions.StocksActions.StocksGetList
{
    public class StocksGetListModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<StocksGetListAction>();
        }
    }
}
