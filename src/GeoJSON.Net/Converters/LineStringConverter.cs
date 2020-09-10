﻿// Copyright © Joerg Battermann 2014, Matt Hunt 2017

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
    public class LineStringConverter : JsonConverter<LineString>
    {
        private readonly PositionEnumerableConverter PositionConverter = new PositionEnumerableConverter();

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
        public override LineString Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject) {
                throw new JsonException();
            }

            LineString lineString = null;

            while (reader.Read()) {
                if (reader.TokenType != JsonTokenType.PropertyName) {
                    continue;
                }

                var propertyName = reader.GetString();

                if (propertyName == "coordinates") {
                    // advance to coordinates
                    reader.Read();

                    // must real all json. cannot exit early
                    lineString = new LineString(new PositionEnumerableConverter()
                                    .Read(ref reader, typeof(IEnumerable<Position>), options));
                }
            }

            return lineString;
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(Utf8JsonWriter writer, LineString value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("coordinates");
            PositionConverter.Write(writer, value.Positions, options);
            writer.WritePropertyName("type");
            writer.WriteStringValue("LineString");
            writer.WriteEndObject();
        }
    }
}
