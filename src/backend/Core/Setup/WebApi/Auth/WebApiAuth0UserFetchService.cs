using Core.Setup.Auth0;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using RestSharp;
using System.Security.Claims;

namespace Core.Setup.WebApi.Auth
{
    public class WebApiAuth0UserFetchService
    {
        private readonly IMemoryCache _memoryCache;

        private readonly MemoryCacheEntryOptions _cacheOptions = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };

        public WebApiAuth0UserFetchService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<CurrentUserInfo> Get(string identifier, KeyValuePair<string, StringValues> bearer, Claim userInfoURL)
        {
            var factory = new Func<ICacheEntry, Task<CurrentUserInfo>>(entry =>
            {
                entry.SetOptions(_cacheOptions);
                return Fetch(bearer, userInfoURL);
            });

            return _memoryCache.GetOrCreateAsync(identifier, factory)!;
        }

        private Task<CurrentUserInfo> Fetch(KeyValuePair<string, StringValues> bearer, Claim userInfoURL)
        {
            var client = new RestClient();
            var request = new RestRequest(userInfoURL.Value);
            request.AddHeader(bearer.Key, bearer.Value);
            return client.GetAsync<CurrentUserInfo>(request)!;
        }

    }
}
