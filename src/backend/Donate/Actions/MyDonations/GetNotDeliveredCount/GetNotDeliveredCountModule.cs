using Autofac;
using Core.Setup.Autofac;
using Donate.Actions.MyDonations.GetList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Actions.MyDonations.GetNotDeliveredCount
{
    public class GetNotDeliveredCountModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<GetNotDeliveredCountAction>();
        }
    }
}
