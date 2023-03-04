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
    private readonly ManageUsersGetListAction _getListAction;
    private readonly ManageUsersGetSingleAction _getSingleAction;

    public ManageUsersController(ManageUsersGetListAction getListAction, ManageUsersGetSingleAction getSingleAction)
    {
        _getListAction = getListAction;
        _getSingleAction = getSingleAction;
    }

    [HttpGet]
    public async Task<ActionResult> GetList(int page, int pageSize, string? filter) => await _getListAction.Execute(page, pageSize, filter);

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSingle(string id) => await _getSingleAction.Execute(id);

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
    public async Task<ActionResult> PatchSingle(string id, [FromBody] DeleteMeManageUserPayload payload)
    {
        var result = new DeleteMeManageUserResponse
        {
            Id = "vb1232fe",
            FullName = "Adam Kowalski",
            Roles = payload.Roles,
            WarehouseId = "batr"
        };

        return await Task.FromResult(Ok(result));
    }
}
