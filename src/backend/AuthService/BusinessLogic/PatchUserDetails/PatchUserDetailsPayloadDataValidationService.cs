using Core.Auth0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic.PatchUserDetails
{
    public class PatchUserDetailsPayloadDataValidationService
    {
        public bool Check(PatchUserDetailsPayload userDetailsPayload)
        {
            var check = userDetailsPayload.FirstName != null;
            check &= userDetailsPayload.LastName != null;

            return check;
        }
    }
}
