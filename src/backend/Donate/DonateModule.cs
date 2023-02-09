using Autofac;
using Donate.Jobs.SetExpiredDonations;

namespace Donate
{
    public class DonateModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterActions(builder);
            RegisterJobs(builder);
        }

        private void RegisterActions(ContainerBuilder builder)
        {

        }

        private void RegisterJobs(ContainerBuilder builder)
        {
            builder.RegisterModule<SetExpiredDonationsModule>();
        }
    }
}
