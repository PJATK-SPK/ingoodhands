using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace WebApi.Controllers.Auth;

[EnableCors]
[ApiController]
[Authorize]
[Route("manage-users")]
public class ManageUsersController : ControllerBase
{
    public class DeleteMeManageUsersResponseItem
    {
        public string Id { get; set; } = default!; // User Id
        public string FullName { get; set; } = default!;
        public List<string> Roles { get; set; } = default!;
    }
    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize, string? filter)
    {
        var appDbContextUsers = new List<DeleteMeManageUsersResponseItem>
        {
            new DeleteMeManageUsersResponseItem
            {
                Id="vb1232fe",
                FullName = "Adam Kowalski",
                Roles= new List<string>
                {
                    "Administrator",
                    "Donor",
                    "Needy",
                }
            },
            new DeleteMeManageUsersResponseItem
            {
                Id="hn78232fe",
                FullName = "Marcin Kowalski",
                Roles= new List<string>
                {
                    "Administrator",
                    "WarehouseKeeper",
                    "Deliverer",
                }
            }
        }.AsQueryable();

        var result = appDbContextUsers.PageResult(page, pageSize);

        return await Task.FromResult(Ok(result));
    }

    public class DeleteMeManageUserResponse
    {
        public string Id { get; set; } = default!; // User Id
        public string FullName { get; set; } = default!;
        public List<string> Roles { get; set; } = default!;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingle(string id)
    {
        var result = new DeleteMeManageUsersResponseItem
        {
            Id = "vb1232fe",
            FullName = "Adam Kowalski",
            Roles = new List<string>
                {
                    "Administrator",
                    "Donor",
                    "Needy",
                }
        };

        return await Task.FromResult(Ok(result));
    }

    public class DeleteMeManageUserPayload
    {
        public List<string> Roles { get; set; } = default!;
    }
    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchSingle(string id, [FromBody] DeleteMeManageUserPayload payload)
    {
        var result = new DeleteMeManageUsersResponseItem
        {
            Id = "vb1232fe",
            FullName = "Adam Kowalski",
            Roles = payload.Roles
        };

        return await Task.FromResult(Ok(result));
    }
}
