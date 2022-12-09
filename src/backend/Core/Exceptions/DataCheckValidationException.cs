using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class DataCheckValidationException : Exception
    {
        public DataCheckValidationException(string message)
         : base(String.Format("Data is invalid at: {0}", message))
        {
        }
    }
}
