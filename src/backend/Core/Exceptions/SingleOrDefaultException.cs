using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class SingleOrDefaultException : Exception
    {
        public SingleOrDefaultException(string message)
            : base(String.Format("Data is null at: {0}", message))
        {
        }
    }
}
