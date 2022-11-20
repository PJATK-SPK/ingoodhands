using Core.Auth0;

namespace TestsCore
{
    public class TestsCurrentUserService : ICurrentUserService
    {
        public CurrentUserInfo? UserInfo { get; set; }

        public void Update(CurrentUserInfo info) => UserInfo = info;

        public string GetUserAuthIdentifier()
        {
            NullGuard();
            return UserInfo!.Identifier!;
        }

        public string GetUserEmail()
        {
            NullGuard();
            return UserInfo!.Email!;
        }

        public Task<CurrentUserInfo> GetUserInfo()
        {
            NullGuard();
            return Task.FromResult(UserInfo!);
        }

        private void NullGuard()
        {
            if (UserInfo == null)
            {
                throw new ArgumentException("Please use toolkit.UpdateUserInfo() in your integration test first!");
            }
        }
    }
}
