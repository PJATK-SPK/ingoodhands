using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Actions.AuthActions.ManageUsersActions
{
    public class ManageUsersResponseItem
    {
        public string Id { get; set; } = default!; // User Id
        public string FullName { get; set; } = default!;
        public string? WarehouseName { get; set; } // can be null
        public List<string> Roles { get; set; } = default!;
    }
}
