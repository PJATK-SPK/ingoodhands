using System.Runtime.Serialization;

namespace Core.Exceptions
{
    /// <summary> For HTTP: 401, Unauthenticated - we don't know this user. </summary>
    [Serializable]
    public class UnauthenticatedException : Exception
    {
        public UnauthenticatedException(string message) : base(message) { }

        protected UnauthenticatedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
