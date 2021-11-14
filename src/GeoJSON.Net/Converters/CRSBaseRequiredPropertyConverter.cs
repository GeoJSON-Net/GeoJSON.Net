using GeoJSON.Net.CoordinateReferenceSystem;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJSON.Net.Converters
{
    public class CRSBaseRequiredPropertyConverter : JsonConverter<CRSBase>
    {
        public override CRSBase Read(
            ref Utf8JsonReader reader,
            Type type,
            JsonSerializerOptions options)
        {
            // Don't pass in options when recursively calling Deserialize.
            var crsBaseClass = JsonSerializer.Deserialize<CRSBase>(ref reader);

            if (crsBaseClass.Type == default)
                throw new JsonException("Required property Type not set in the JSON");

            if (crsBaseClass.Properties == default)
                throw new JsonException("Required property Properties not set in the JSON");

            // Check for required fields set by values in JSON
            return crsBaseClass;
        }

        public override void Write(
            Utf8JsonWriter writer,
            CRSBase crsBaseClass,
            JsonSerializerOptions options)
        {
            // Don't pass in options when recursively calling Serialize.
            JsonSerializer.Serialize(writer, crsBaseClass);
        }
    }
}