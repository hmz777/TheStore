using System.Text.Json;
using System.Text.Json.Serialization;

namespace TheStore.SharedModels.Models
{
    public class ResultJsonConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert != typeof(Result))
                return false;

            if (typeToConvert.GetGenericTypeDefinition() != typeof(Result<>))
                return false;

            return true;
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private class ResultJsonConverterInner : JsonConverter<Result>
        {
            public override Result? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }
        }

        private class GenericResultJsonConverterInner : JsonConverter<Result>
        {
            public override Result? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }
        }
    }
}
