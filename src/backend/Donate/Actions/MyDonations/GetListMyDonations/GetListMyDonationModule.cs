using Autofac;
using Core.Setup.Autofac;
using Donate.Actions.MyDonations.GetList;

namespace Donate.Actions.MyDonations.GetListMyDonations
{
    public class GetListMyDonationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<GetListMyDonationAction>();
            builder.RegisterAsScoped<GetListMyDonationService>();
        }
    }
}
