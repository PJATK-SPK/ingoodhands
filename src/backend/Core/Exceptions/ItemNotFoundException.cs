using System.Runtime.Serialization;

namespace Core.Exceptions
{
    /// <summary> For HTTP: 404. Item was not found </summary>
    [Serializable]
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) : base(message) { }

        protected ItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
