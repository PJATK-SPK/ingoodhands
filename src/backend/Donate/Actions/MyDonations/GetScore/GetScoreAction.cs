using Core.Database;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Services;
using Core.Setup.Auth0;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Actions.MyDonations.GetScore
{
    public class GetScoreAction
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;

        public GetScoreAction(AppDbContext appDbContext, ICurrentUserService currentUserService, GetCurrentUserService getCurrentUserService)
        {
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            int scorePerDonation = 1000;
            int scorePerProduct = 10;

            var totalScore = await _appDbContext.Donations
                .Where(d => d.CreationUserId == currentUser.Id && d.IsDelivered)
                .Select(d => scorePerDonation + (d.Products!.Sum(dp => dp.Quantity) * scorePerProduct))
                .SumAsync();

            return new OkObjectResult(new { Score = totalScore });
        }
    }
}
