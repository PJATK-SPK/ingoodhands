using Autofac;
using Core.Setup.Autofac;
using Orders.Actions.RequestHelpActions.RequestHelpGetMap;

namespace Orders.Actions.RequestHelpActions.RequestHelpGetMap
{
    internal class RequestHelpGetMapModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<RequestHelpGetMapAction>();
            builder.RegisterAsScoped<RequestHelpGetMapService>();
        }
    }
}