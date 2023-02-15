using System.Runtime.Serialization;

namespace Core.Exceptions
{
    /// <summary> For HTTP: 500. Our fault. </summary>
    [Serializable]
    public class ApplicationErrorException : Exception
    {
        public ApplicationErrorException(string message) : base($"{message}. Please, contact system administrator") { }

        protected ApplicationErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}