using System.Runtime.Serialization;

namespace Core.Exceptions
{
    [Serializable]
    public class HttpError500Exception : Exception
    {
        public HttpError500Exception(string message) : base($"{message}. Please, contact system administrator") { }

        protected HttpError500Exception(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}