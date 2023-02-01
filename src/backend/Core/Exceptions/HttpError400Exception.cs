using System.Runtime.Serialization;

namespace Core.Exceptions
{
    [Serializable]
    public class HttpError400Exception : Exception
    {
        public HttpError400Exception(string message)
            : base($"{message}. Please, contact system administrator")
        {
        }
    }
}
