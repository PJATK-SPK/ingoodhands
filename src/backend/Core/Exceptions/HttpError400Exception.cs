using System.Runtime.Serialization;

namespace Core.Exceptions
{
    [Serializable]
    public class HttpError400Exception : Exception
    {
        public HttpError400Exception(string message) : base(message) { }

        protected HttpError400Exception(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
