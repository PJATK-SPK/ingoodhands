using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Seeders;
using Core.Services;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Auth.Actions.ManageUsersActions.ManageUsersGetList
{
    public class ManageUsersGetListAction
    {
        private readonly AppDbContext _appDbContext;
        private readonly RoleService _roleService;
        private readonly Hashids _hashids;

        public ManageUsersGetListAction(
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
                .Include(c => c.Warehouse)
                .Where(c => c.Id != UserSeeder.ServiceUser.Id);

            if (filter != null)
            {
                dbResult = dbResult.Where(u => (u.FirstName + " " + u.LastName).ToLower().Contains(filter.ToLower()));
            }

            var result = dbResult.PageResult(page, pageSize);

            var mapped = result.Queryable.Select(c => new ManageUsersGetListResponseItem
            {
                Id = _hashids.EncodeLong(c.Id),
                FullName = c.FirstName + " " + c.LastName,
                Email = c.Email,
                WarehouseId = c.WarehouseId == null ? null : c.WarehouseId.ToString(),
                Roles = c.Roles!.Select(c => c.Role!.Name.ToString()).ToList()
            }).ToList();

            var response = new PagedResult<ManageUsersGetListResponseItem>()
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