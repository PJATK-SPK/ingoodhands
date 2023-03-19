using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.RequestHelpActions.RequestHelpGetMap
{
    public class RequestHelpGetMapModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<RequestHelpGetMapAction>();
            builder.RegisterAsScoped<RequestHelpGetMapService>();
        }
    }
}