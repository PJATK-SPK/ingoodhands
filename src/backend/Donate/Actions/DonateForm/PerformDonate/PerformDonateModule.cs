using Autofac;
using Core.Setup.Autofac;
using Donate.Actions.DonateForm.GetProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Actions.DonateForm.PerformDonate
{
    public class PerformDonateModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<PerformDonateAction>();
            builder.RegisterAsScoped<PerformDonateService>();
            builder.RegisterAsScoped<PerformDonatePayload>();
            builder.RegisterAsScoped<PerformDonateProductPayload>();
            builder.RegisterAsScoped<PerformDonateResponse>();
        }
    }
}
