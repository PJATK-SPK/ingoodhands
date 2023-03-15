using Autofac;
using Core.Setup.Autofac;

namespace Donate.Actions.DonateForm.PerformDonate
{
    public class PerformDonateModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<PerformDonateAction>();
            builder.RegisterAsScoped<PerformDonateService>();
        }
    }
}
