// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System;
using GeoJSON.Net.Geometry;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    ///     Converter to read and write an <see cref="IPosition" />, that is,
    ///     the coordinates of a <see cref="Point" />.
    /// </summary>
    public class MultiLineStringConverter : JsonConverter<MultiLineString>
    {
        private static readonly LineStringEnumerableConverter Converter = new LineStringEnumerableConverter();

        /// <summary>
        ///     Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        ///     The object value.
        /// </returns>
        public override MultiLineString Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject) {
                throw new JsonException();
            }

            MultiLineString multiLineString = null;

            while (reader.Read()) {
                if (reader.TokenType != JsonTokenType.PropertyName) {
                    continue;
                }

                var propertyName = reader.GetString();

                if (propertyName == "coordinates") {
                    // advance to coordinates
                    reader.Read();

                    // must real all json. cannot exit early
                    multiLineString = new MultiLineString(Converter.Read(ref reader, typeof(IEnumerable<LineString>), options));
                }
            }

            return multiLineString;
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(Utf8JsonWriter writer, MultiLineString value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", "MultiLineString");
            writer.WritePropertyName("coordinates");
            Converter.Write(writer, value.LineStrings, options);
            writer.WriteEndObject();
        }
    }
}
