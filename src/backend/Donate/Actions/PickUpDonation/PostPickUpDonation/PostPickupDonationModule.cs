using Autofac;
using Core.Setup.Autofac;

namespace Donate.Actions.PickUpDonation.PostPickUpDonation
{
    public class PostPickupDonationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<PostPickupDonationAction>();
            builder.RegisterAsScoped<PostPickupDonationService>();
        }
    }
}