using System.Runtime.Serialization;

namespace Core.Exceptions
{
    /// <summary> For HTTP: 403, Unauthorized - we know this user, but has no access. </summary>
    [Serializable]
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }

        protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
