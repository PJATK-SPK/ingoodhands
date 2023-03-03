using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Services;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Auth.Actions.ManageUsersActions.GetList
{
    public class GetListAction
    {
        private readonly AppDbContext _appDbContext;
        private readonly RoleService _roleService;
        private readonly Hashids _hashids;

        public GetListAction(
            AppDbContext appDbContext,
            RoleService roleService,
            Hashids hashids
            )
        {
            _appDbContext = appDbContext;
            _roleService = roleService;
            _hashids = hashids;
        }

        public async Task<OkObjectResult> Execute(int page, int pageSize, string? filter = null)
        {
            await _roleService.ThrowIfNoRole(RoleName.Administrator);

            IQueryable<User> dbResult = _appDbContext.Users
                 .Include(c => c.Roles)!
                     .ThenInclude(c => c.Role)
                 .Include(c => c.Warehouse);

            if (filter != null)
            {
                dbResult = dbResult.Where(u => (u.FirstName + " " + u.LastName).Contains(filter));
            }

            var result = dbResult.PageResult(page, pageSize);

            var mapped = result.Queryable.Select(c => new GetListResponseItem
            {
                Id = _hashids.EncodeLong(c.Id),
                FullName = c.FirstName + " " + c.LastName,
                WarehouseName = c.Warehouse == null ? null : c.Warehouse.ShortName,
                Roles = c.Roles!.Select(c => c.Role!.Name.ToString()).ToList()
            }).ToList();

            var response = new PagedResult<GetListResponseItem>()
            {
                CurrentPage = result.CurrentPage,
                PageCount = result.PageCount,
                PageSize = result.PageSize,
                Queryable = mapped.AsQueryable(),
                RowCount = result.RowCount
            };

            return new OkObjectResult(response);
        }
    }
}