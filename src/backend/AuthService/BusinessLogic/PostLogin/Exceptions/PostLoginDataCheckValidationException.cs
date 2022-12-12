using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic.PostLogin.Exceptions
{
    public class PostLoginDataCheckValidationException : Exception
    {
        public PostLoginDataCheckValidationException(string message)
         : base(String.Format("Data is invalid at: {0}", message))
        {
        }
    }
}
