using System.Runtime.Serialization;

namespace Core.Exceptions
{
    /// <summary> For HTTP: 400. Wrong input data. </summary>
    [Serializable]
    public class ClientInputErrorException : Exception
    {
        public ClientInputErrorException(string message) : base(message) { }

        protected ClientInputErrorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
