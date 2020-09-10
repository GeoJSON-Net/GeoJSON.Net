// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using GeoJSON.Net.Geometry;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    /// Converter to read and write the <see cref="IEnumerable{IPosition}" /> type.
    /// </summary>
    public class PositionEnumerableConverter : JsonConverter<IEnumerable<IPosition>>
    {
        private static readonly PositionConverter Converter = new PositionConverter();

        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnumerable<IPosition>).IsAssignableFromType(objectType);
        }

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
        public override IEnumerable<IPosition> Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var positions = new List<IPosition>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.StartArray)
                {
                    positions.Add(Converter.Read(ref reader, typeof(IPosition), options));
                }
                else if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }
            }

            return positions;
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(Utf8JsonWriter writer, IEnumerable<IPosition> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var position in value)
            {
                Converter.Write(writer, position, options);
            }

            writer.WriteEndArray();
        }
    }
}
