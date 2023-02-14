using Autofac;
using Core.Setup.Autofac;

namespace Donate.Jobs.SetExpiredDonations
{
    public class SetExpiredDonationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<SetExpiredDonationsJob>();
        }
    }
}
