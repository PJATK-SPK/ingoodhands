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

        private static void RegisterActions(ContainerBuilder builder)
        {
            // Will be used in future
        }

        private static void RegisterJobs(ContainerBuilder builder)
        {
            builder.RegisterModule<SetExpiredDonationsModule>();
        }
    }
}
