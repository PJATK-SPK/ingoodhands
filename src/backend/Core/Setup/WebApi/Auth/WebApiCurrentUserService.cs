using Core.Exceptions;
using Core.Setup.Auth0;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Core.Setup.WebApi.Auth
{
    public class WebApiCurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly WebApiAuth0UserFetchService _fetchService;

        public WebApiCurrentUserService(IHttpContextAccessor contextAccessor, WebApiAuth0UserFetchService fetchService)
        {
            _contextAccessor = contextAccessor;
            _fetchService = fetchService;
        }

        public string GetUserEmail() => GetClaims().Single(c => c.Type.Contains("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")).Value;
        public string GetUserAuthIdentifier() => GetClaims().Single(c => c.Type.Contains("nameidentifier")).Value;

        public async Task<CurrentUserInfo> GetUserInfo()
        {
            var claims = GetClaims();
            var userInfoURL = claims.Single(c => c.Value.Contains("userinfo"));
            var bearer = _contextAccessor.HttpContext!.Request.Headers.Single(c => c.Key == "Authorization");
            var result = await _fetchService.Get(GetUserAuthIdentifier(), bearer, userInfoURL);

            if (result == null || result.Identifier == null)
                throw new ApplicationErrorException("There was an error during downloading user info");

            return result;
        }

        private IEnumerable<Claim> GetClaims()
            => _contextAccessor.HttpContext!.User.Claims;
    }
}
