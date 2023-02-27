using Autofac;
using Core.Setup.Autofac;

namespace Auth.Actions.UserSettingsActions.PatchUserDetails
{
    public class PatchUserDetailsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<PatchUserDetailsAction>();
            builder.RegisterAsScoped<PatchUserDetailsService>();
            builder.RegisterAsScoped<PatchUserDetailsPayloadValidator>();
        }
    }
}
