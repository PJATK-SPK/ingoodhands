using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

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

        public async Task<PagedResult<GetListMyDonationsItemResponse>> GetListMyDonations(int page, int pageSize)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            await _roleService.ThrowIfNoRole(RoleName.Donor, currentUser.Id);

            var dbResult = _appDbContext.Donations
                .Include(c => c.Products)
                .Where(c =>
                    c.CreationUserId == currentUser.Id &&
                    c.Status == DbEntityStatus.Active || c.Status == DbEntityStatus.Inactive)
                .OrderByDescending(c => c.CreationDate)
                .PageResult(page, pageSize);

            var mapped = dbResult.Queryable.Select(c => new GetListMyDonationsItemResponse
            {
                Id = _hashids.EncodeLong(c.Id),
                Name = c.Name,
                ProductsCount = c.Products!.Count,
                CreationDate = c.CreationDate,
                IsDelivered = c.IsDelivered,
                IsExpired = c.IsExpired
            });

            var result = new PagedResult<GetListMyDonationsItemResponse>()
            {
                CurrentPage = dbResult.CurrentPage,
                PageCount = dbResult.PageCount,
                PageSize = dbResult.PageSize,
                Queryable = mapped,
                RowCount = dbResult.RowCount
            };

            return result;
        }
    }
}
