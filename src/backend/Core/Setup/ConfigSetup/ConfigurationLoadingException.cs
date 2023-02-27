using System.Runtime.Serialization;

namespace Core.Setup.ConfigSetup
{
    [Serializable]
    public class ConfigurationLoadingException : Exception
    {
        public ConfigurationLoadingException(string message) : base(message) { }

        protected ConfigurationLoadingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}