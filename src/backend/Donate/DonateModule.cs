using Autofac;
using Core.Setup.Autofac;
using Donate.Actions.DonateForm.GetProducts;
using Donate.Actions.DonateForm.GetWarehouses;
using Donate.Actions.DonateForm.PerformDonate;
using Donate.Actions.MyDonations.GetDetails;
using Donate.Actions.MyDonations.GetListMyDonations;
using Donate.Actions.MyDonations.GetNotDeliveredCount;
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
            builder.RegisterModule<GetWarehousesModule>();
            builder.RegisterModule<GetProductsModule>();
            builder.RegisterModule<PerformDonateModule>();
            builder.RegisterModule<GetListMyDonationModule>();
            builder.RegisterModule<GetMyDonationDetailsModule>();
            builder.RegisterAsScoped<GetNotDeliveredCountAction>();
        }

        private static void RegisterJobs(ContainerBuilder builder)
        {
            builder.RegisterModule<SetExpiredDonationsModule>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<DonateNameBuilderService>();
        }
    }
}
