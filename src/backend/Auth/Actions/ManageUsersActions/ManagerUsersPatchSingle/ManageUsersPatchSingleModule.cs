using Auth.Actions.ManageUsersActions.ManageUsersGetList;
using Autofac;
using Core.Setup.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Actions.ManageUsersActions.ManagerUsersPatchSingle
{
    public class ManageUsersPatchSingleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<ManageUsersPatchSingleAction>();
            builder.RegisterAsScoped<ManageUsersPatchSingleService>();
        }
    }
}
