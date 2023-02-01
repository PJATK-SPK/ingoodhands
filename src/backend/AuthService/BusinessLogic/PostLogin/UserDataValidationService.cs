using Core.Auth0;

namespace AuthService.BusinessLogic.PostLogin
{
    public class UserDataValidationService
    {
        public bool Check(CurrentUserInfo currentAuth0UserInfo)
        {
            var check = currentAuth0UserInfo.Email != null;
            check &= currentAuth0UserInfo.EmailVerified == true;
            check &= currentAuth0UserInfo.Identifier != null;
            check &= currentAuth0UserInfo.FamilyName != null;
            check &= currentAuth0UserInfo.GivenName != null;
            check &= currentAuth0UserInfo.Name != null;
            check &= currentAuth0UserInfo.Nickname != null;

            return check;
        }
    }
}
