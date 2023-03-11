using Core.Database;
using Core.Services;
using Core.Setup.Auth0;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Donate.Actions.MyDonations.GetNotDeliveredCount
{
    public class GetNotDeliveredCountAction
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;

        public GetNotDeliveredCountAction(AppDbContext appDbContext, ICurrentUserService currentUserService, GetCurrentUserService getCurrentUserService)
        {
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            var numberOfNotDeliveredDonations = await _appDbContext.Donations
                .Where(c => c.CreationUserId == currentUser.Id && !c.IsDelivered && !c.IsExpired)
                .CountAsync();

            return new OkObjectResult(new GetNotDeliveredCountResponse { Count = numberOfNotDeliveredDonations });
        }
    }
}
