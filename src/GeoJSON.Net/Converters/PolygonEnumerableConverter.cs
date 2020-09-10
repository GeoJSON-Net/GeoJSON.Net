// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using GeoJSON.Net.Geometry;

namespace GeoJSON.Net.Converters {
    /// <summary>
    /// Converter to read and write the <see cref="IEnumerable{MultiPolygon}" /> type.
    /// </summary>
    public class PolygonEnumerableConverter : JsonConverter<IEnumerable<Polygon>>
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
        public override IEnumerable<Polygon> Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var polygons = new List<Polygon>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.StartArray)
                {
                    polygons.Add(new Polygon(Converter.Read(ref reader, type, options)));
                } else if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }
            }

            return polygons;
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(Utf8JsonWriter writer, IEnumerable<Polygon> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var polygon in value)
            {
                Converter.Write(writer, polygon.Rings, options);
            }
            writer.WriteEndArray();
        }
    }
}
