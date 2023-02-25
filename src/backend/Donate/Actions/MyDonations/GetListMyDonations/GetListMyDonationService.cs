using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using Donate.Actions.DonateForm.GetWarehouses;
using HashidsNet;
using Microsoft.Extensions.Logging;

namespace Donate.Actions.MyDonations.GetList
{
    public class GetListMyDonationService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly RoleService _roleService;
        private readonly Hashids _hashids;

        public GetListMyDonationService(
            AppDbContext appDbContext,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            RoleService roleService,
            Hashids hashids
            )
        {
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _roleService = roleService;
            _hashids = hashids;
        }

        public async Task<List<GetListMyDonationsItemResponse>> GetListMyDonations()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);
            await _roleService.ThrowIfNoRole(RoleName.Donor, currentUser.Id);

            var listOfDonations = _appDbContext.Donations.Where(c => c.CreationUserId == currentUser.Id
            && c.Status == DbEntityStatus.Active || c.Status == DbEntityStatus.Inactive).ToList();

            if (!listOfDonations.Any())
            {
                return new List<GetListMyDonationsItemResponse>();
            }

            var response = listOfDonations.Select(c => new GetListMyDonationsItemResponse
            {
                Id = _hashids.EncodeLong(c.Id),
                Name = c.Name,
                ProductsCount = 5,
                CreationDate = DateTime.UtcNow,
                IsDelivered = true,
                IsExpired = false
            }).ToList();

            return response;
        }
    }
}
