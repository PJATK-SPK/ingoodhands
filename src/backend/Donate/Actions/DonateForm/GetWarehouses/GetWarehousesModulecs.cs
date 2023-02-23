using Autofac;
using Core.Setup.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Actions.DonateForm.GetWarehouses
{
    public class GetWarehousesModulecs : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<GetWarehousesAction>();
            builder.RegisterAsScoped<GetWarehousesService>();
        }
    }
}
