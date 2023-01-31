using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class HttpError500Exception : Exception
    {
        public HttpError500Exception(string message)
            : base($"{message}. Please, contact system administrator")
        {
        }
    }
}