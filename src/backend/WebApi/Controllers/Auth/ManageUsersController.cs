using Auth.Actions.AuthActions.ManageUsersActions;
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
    private readonly ManageUsersAction _manageUsersAction;

    public ManageUsersController(ManageUsersAction manageUsersAction)
    {
        _manageUsersAction = manageUsersAction;
    }

    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize, string? filter) => await _manageUsersAction.Execute(page, pageSize, filter);

    public class DeleteMeManageUserResponse
    {
        public string Id { get; set; } = default!; // User Id
        public string FullName { get; set; } = default!;
        public string? WarehouseName { get; set; } // can be null
        public List<string> Roles { get; set; } = default!;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingle(string id)
    {
        var result = new ManageUsersResponseItem
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
        public string WarehouseName { get; set; } = default!;
        public List<string> Roles { get; set; } = default!;
    }
    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchSingle(string id, [FromBody] DeleteMeManageUserPayload payload)
    {
        var result = new ManageUsersResponseItem
        {
            Id = "vb1232fe",
            FullName = "Adam Kowalski",
            Roles = payload.Roles
        };

        return await Task.FromResult(Ok(result));
    }
}
