using Autofac;
using Auth.Services;
using Auth.Actions.AuthActions.PostLogin;
using Auth.Actions.UserSettingsActions.PatchUserDetails;
using Auth.Actions.UserSettingsActions.GetAuth0UsersByCurrentUser;
using Auth.Actions.UserSettingsActions.GetUserDetails;
using Core.Setup.Autofac;
using Auth.Actions.ManageUsersActions.ManageUsersGetList;
using Auth.Actions.ManageUsersActions.ManageUsersGetSingle;
using Auth.Actions.ManageUsersActions.ManagerUsersPatchSingle;

namespace Auth
{
    public class AuthModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterActions(builder);
            RegisterServices(builder);
        }

        private static void RegisterActions(ContainerBuilder builder)
        {
            builder.RegisterModule<PostLoginModule>();
            builder.RegisterModule<GetUserDetailsModule>();
            builder.RegisterModule<PatchUserDetailsModule>();
            builder.RegisterModule<ManageUsersGetListModule>();
            builder.RegisterModule<ManageUsersGetSingleModule>();
            builder.RegisterModule<ManageUsersPatchSingleModule>();
            builder.RegisterModule<GetAuth0UsersByCurrentUserModule>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<CurrentUserInfoValidator>();
        }
    }
}
