using Core.Auth0;
using Microsoft.AspNetCore.Http;
using RestSharp;
using System.Security.Claims;

namespace Core.WebApi.Auth
{
    public class WebApiCurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public WebApiCurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserEmail() => GetClaims().Single(c => c.Type.Contains("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")).Value;
        public string GetUserAuthIdentifier() => GetClaims().Single(c => c.Type.Contains("nameidentifier")).Value;

        public async Task<CurrentUserInfo> GetUserInfo()
        {
            var claims = GetClaims();
            var userInfoURL = claims.Single(c => c.Value.Contains("userinfo"));
            var bearer = _contextAccessor.HttpContext!.Request.Headers.Single(c => c.Key == "Authorization");
            var client = new RestClient();
            var request = new RestRequest(userInfoURL.Value);
            request.AddHeader(bearer.Key, bearer.Value);
            var result = await client.GetAsync<CurrentUserInfo>(request);
            if (result == null || result.Identifier == null) throw new HttpRequestException("There was an error during downloading user info");
            return result;
        }

        private IEnumerable<Claim> GetClaims()
            => _contextAccessor.HttpContext!.User.Claims;
    }
}
