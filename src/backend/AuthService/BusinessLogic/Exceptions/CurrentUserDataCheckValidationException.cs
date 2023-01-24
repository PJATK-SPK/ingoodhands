using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic.Exceptions
{
    public class CurrentUserDataCheckValidationException : Exception
    {
        public CurrentUserDataCheckValidationException(string message)
         : base(string.Format("Data is invalid at: {0}", message))
        {
        }
    }
}
