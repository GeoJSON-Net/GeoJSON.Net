// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using GeoJSON.Net.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    /// Converter to read and write the <see cref="IEnumerable{MultiPolygon}" /> type.
    /// </summary>
    public class PolygonEnumerableConverter : JsonConverter<IEnumerable<Polygon>>
    {

        private static readonly LineStringEnumerableConverter PolygonConverter = new LineStringEnumerableConverter();
        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnumerable<Polygon>).IsAssignableFromType(objectType);
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
        public override IEnumerable<Polygon> Read(
            ref Utf8JsonReader reader,
            Type type,
            JsonSerializerOptions options)
        {
            JsonElement polygons;
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;
                case JsonTokenType.StartArray:
                    polygons = JsonDocument.ParseValue(ref reader).RootElement;
                    break;
                default:
                    throw new InvalidOperationException("Incorrect json type");
            }

            var enumerator = polygons.EnumerateArray();
            var lineStrings = new List<IEnumerable<LineString>>();
            int i = 0;
            while (enumerator.MoveNext())
            {
                var element = enumerator.Current;
                byte[] bytes = Encoding.UTF8.GetBytes(element.GetRawText());
                var stream = new MemoryStream(bytes);

                var buffer = new byte[stream.Length];

                // Fill the buffer.
                // For this snippet, we're assuming the stream is open and has data.
                // If it might be closed or empty, check if the return value is 0.
                stream.Read(buffer, 0, (int)stream.Length);

                var elementReader = new Utf8JsonReader(buffer, isFinalBlock: false, state: default);

                lineStrings.Add(PolygonConverter.Read(
                            ref elementReader,
                            typeof(IEnumerable<LineString>),
                            options));
                i++;
            }

            return lineStrings.Select(lines => new Polygon(lines));
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>

        public override void Write(
            Utf8JsonWriter writer,
            IEnumerable<Polygon> value,
            JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var polygon in value)
            {
                PolygonConverter.Write(writer, polygon.Coordinates, options);
            }
            writer.WriteEndArray();
        }
    }
}