using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Core.Json
{
    public class JsonDateTimeNullConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateTime));
            var readed = reader.GetString();

            if (readed == null)
            {
                return null;
            }

            var result = DateTime.Parse(readed);
            result = result.ToUniversalTime();
            return result;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteStringValue(value.Value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
            }
        }
    }
}
