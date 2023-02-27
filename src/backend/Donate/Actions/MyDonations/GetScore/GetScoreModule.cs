using Autofac;
using Core.Setup.Autofac;
using Donate.Actions.MyDonations.GetNotDeliveredCount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Actions.MyDonations.GetScore
{
    public class GetScoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<GetScoreAction>();
        }
    }
}
