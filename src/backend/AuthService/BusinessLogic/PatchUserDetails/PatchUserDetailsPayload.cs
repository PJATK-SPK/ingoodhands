using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic.PatchUserDetails
{
    public class PatchUserDetailsPayload
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
