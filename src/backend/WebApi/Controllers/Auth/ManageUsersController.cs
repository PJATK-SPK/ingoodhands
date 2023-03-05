using Auth.Actions.ManageUsersActions.ManagerUsersPatchSingle;
using Auth.Actions.ManageUsersActions.ManageUsersGetList;
using Auth.Actions.ManageUsersActions.ManageUsersGetSingle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Auth;

[EnableCors]
[ApiController]
[Authorize]
[Route("manage-users")]
public class ManageUsersController : ControllerBase
{
    private readonly ManageUsersGetListAction _manageUsersGetListAction;
    private readonly ManageUsersGetSingleAction _manageUsersGetSingleAction;
    private readonly ManageUsersPatchSingleAction _manageUsersPatchSingleAction;

    public ManageUsersController(
        ManageUsersGetListAction manageUsersGetListAction,
        ManageUsersGetSingleAction manageUsersGetSingleAction,
        ManageUsersPatchSingleAction manageUsersPatchSingleAction
        )
    {
        _manageUsersGetListAction = manageUsersGetListAction;
        _manageUsersGetSingleAction = manageUsersGetSingleAction;
        _manageUsersPatchSingleAction = manageUsersPatchSingleAction;
    }

    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize, string? filter) => await _manageUsersGetListAction.Execute(page, pageSize, filter);

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingle(string id) => await _manageUsersGetSingleAction.Execute(id);

    public class DeleteMeManageUserPayload
    {
        public string? WarehouseId { get; set; }
        public List<string> Roles { get; set; } = default!;
    }
    public class DeleteMeManageUserResponse
    {
        public string Id { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string? WarehouseId { get; set; }
        public List<string> Roles { get; set; } = default!;
    }
    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchSingle(string id, [FromBody] ManageUsersPatchSinglePayload payload)
        => await _manageUsersPatchSingleAction.Execute(id, payload);
}
