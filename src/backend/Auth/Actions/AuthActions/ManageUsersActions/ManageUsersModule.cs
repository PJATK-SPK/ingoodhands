using Auth.Actions.UserSettingsActions.GetAuth0UsersByCurrentUser;
using Autofac;
using Core.Setup.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Actions.AuthActions.ManageUsersActions
{
    internal class ManageUsersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<ManageUsersAction>();
        }
    }
}
