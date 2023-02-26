using Autofac;
using Core.Setup.Autofac;

namespace Donate.Actions.MyDonations.GetDetails
{
    public class GetMyDonationDetailsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<GetMyDonationDetailsAction>();
            builder.RegisterAsScoped<GetMyDonationDetailsService>();
        }
    }
}
