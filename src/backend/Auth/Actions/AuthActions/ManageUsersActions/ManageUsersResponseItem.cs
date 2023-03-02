using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Actions.AuthActions.ManageUsersActions
{
    public class ManageUsersResponseItem
    {
        public string Id { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string? WarehouseName { get; set; }
        public List<string> Roles { get; set; } = default!;
    }
}
