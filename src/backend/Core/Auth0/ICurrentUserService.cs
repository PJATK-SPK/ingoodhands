namespace Core.Auth0
{
    public interface ICurrentUserService
    {
        string GetUserEmail();
        string GetUserAuthIdentifier();
        Task<CurrentUserInfo> GetUserInfo();
    }
}
