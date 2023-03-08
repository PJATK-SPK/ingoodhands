using Autofac;
using Core.Setup.Autofac;
using Donate.Actions.DonateForm.GetProducts;
using Donate.Actions.DonateForm.GetWarehouses;
using Donate.Actions.DonateForm.PerformDonate;
using Donate.Actions.MyDonations.GetDetails;
using Donate.Actions.MyDonations.GetListMyDonations;
using Donate.Actions.MyDonations.GetNotDeliveredCount;
using Donate.Actions.MyDonations.GetScore;
using Donate.Actions.PickUpDonation.PostPickUpDonation;
using Donate.Jobs.IncludeToStock;
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
            builder.RegisterModule<GetScoreModule>();
            builder.RegisterModule<GetProductsModule>();
            builder.RegisterModule<PerformDonateModule>();
            builder.RegisterModule<GetWarehousesModule>();
            builder.RegisterModule<GetListMyDonationModule>();
            builder.RegisterModule<PostPickupDonationModule>();
            builder.RegisterModule<GetMyDonationDetailsModule>();
            builder.RegisterModule<GetNotDeliveredCountModule>();
        }

        private static void RegisterJobs(ContainerBuilder builder)
        {
            builder.RegisterModule<SetExpiredDonationsModule>();
            builder.RegisterModule<IncludeToStockModule>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<DonateNameBuilderService>();
        }
    }
}
