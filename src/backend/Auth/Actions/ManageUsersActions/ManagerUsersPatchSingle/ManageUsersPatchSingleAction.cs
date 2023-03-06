using Auth.Actions.ManageUsersActions.ManageUsersGetSingle;
using Auth.Actions.ManageUsersActions.ManageUsersPatchSingle;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Exceptions;
using Core.Services;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Actions.ManageUsersActions.ManagerUsersPatchSingle
{
    public class ManageUsersPatchSingleAction
    {
        private readonly ManageUsersPatchSingleService _manageUsersPatchSingleService;

        public ManageUsersPatchSingleAction(
            ManageUsersPatchSingleService manageUsersPatchSingleService)
        {
            _manageUsersPatchSingleService = manageUsersPatchSingleService;
        }

        public async Task<OkObjectResult> Execute(string id, ManageUsersPatchSinglePayload payload)
        {
            var result = await _manageUsersPatchSingleService.PatchUserRolesAndWarehouseId(id, payload);

            return new OkObjectResult(result);
        }
    }
}
