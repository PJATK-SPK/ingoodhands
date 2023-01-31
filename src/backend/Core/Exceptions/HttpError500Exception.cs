using System.Runtime.Serialization;

namespace Core.Exceptions
{
    [Serializable]
    public class HttpError500Exception : Exception, ISerializable
    {
        public HttpError500Exception(string message)
            : base($"{message}. Please, contact system administrator")
        {
        }
    }
}