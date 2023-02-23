using Autofac;
using Core.Setup.Autofac;
using Donate.Actions.DonateForm.GetWarehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Actions.DonateForm.GetProducts
{
    public class GetProductsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<GetProductsAction>();
            builder.RegisterAsScoped<GetProductsService>();
        }
    }
}
