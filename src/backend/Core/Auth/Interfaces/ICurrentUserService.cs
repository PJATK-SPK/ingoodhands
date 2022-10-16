using Core.Auth.Models;

namespace Core.Auth.Interfaces
{
    public interface ICurrentUserService
    {
        string GetUserEmail();
        string GetUserAuthIdentifier();
        Task<CurrentUserInfo> GetUserInfo();
    }
}
