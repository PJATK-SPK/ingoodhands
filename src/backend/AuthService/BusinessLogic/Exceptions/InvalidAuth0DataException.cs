using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic.Exceptions
{
    [Serializable]
    public class InvalidAuth0DataException : Exception
    {
        public InvalidAuth0DataException()
         : base(string.Format("Your Auth0User data is invalid, please contact system administrator")) { }
    }
}
