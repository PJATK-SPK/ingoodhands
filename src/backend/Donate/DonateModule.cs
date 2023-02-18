using Autofac;
using Core.Setup.Autofac;
using Donate.Jobs.SetExpiredDonations;
using Donate.Services.DonateNameBuilder;
using Donate.Shared;

namespace Donate
{
    public class DonateModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterActions(builder);
            RegisterJobs(builder);
            RegisterServices(builder);
        }

        private static void RegisterActions(ContainerBuilder builder)
        {
            // Will be used in future
        }

        private static void RegisterJobs(ContainerBuilder builder)
        {
            builder.RegisterModule<SetExpiredDonationsModule>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<DonateNameBuilderService>();
            builder.RegisterAsScoped<ExpireDateService>();
        }
    }
}
