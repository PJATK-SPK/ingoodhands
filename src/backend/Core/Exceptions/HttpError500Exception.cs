using System.Runtime.Serialization;

namespace Core.Exceptions
{
    [Serializable]
    public class HttpError500Exception : Exception
    {
        public HttpError500Exception()
        {
        }

        public HttpError500Exception(string message)
            : base($"{message}. Please, contact system administrator")
        {
        }

        public HttpError500Exception(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public HttpError500Exception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}