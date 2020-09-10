// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using GeoJSON.Net.Geometry;

namespace GeoJSON.Net.Converters {
    /// <summary>
    /// Converter to read and write the <see cref="IEnumerable{LineString}" /> type.
    /// </summary>
    public class LineStringEnumerableConverter : JsonConverter<IEnumerable<LineString>>
    {
        private static readonly PositionEnumerableConverter Converter = new PositionEnumerableConverter();

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnumerable<LineString>).IsAssignableFromType(objectType);
        }
        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override IEnumerable<LineString> Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var lineStrings = new List<LineString>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.StartArray)
                {
                    lineStrings.Add(new LineString(Converter.Read(ref reader, type, options)));
                } else if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }
            }

            return lineStrings;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(Utf8JsonWriter writer, IEnumerable<LineString> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var lineString in value)
            {
                Converter.Write(writer, lineString.Positions, options);
            }
            writer.WriteEndArray();
        }
    }
}
