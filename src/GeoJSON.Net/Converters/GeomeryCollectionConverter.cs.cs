// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System;
using GeoJSON.Net.Geometry;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;

namespace GeoJSON.Net.Converters {
    /// <summary>
    ///     Converter to read and write an <see cref="IPosition" />, that is,
    ///     the coordinates of a <see cref="Point" />.
    /// </summary>
    public class GeometryCollectionConverter : JsonConverter<GeometryCollection> {
        private static readonly GeometryConverter Converter = new GeometryConverter();

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
        public override GeometryCollection Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options) {
            var geometries = new List<IGeometryObject>();

            if (reader.TokenType == JsonTokenType.EndObject) {
                return new GeometryCollection(geometries);
            }

            while (reader.Read()) {
                if (reader.TokenType == JsonTokenType.EndArray) {
                    reader.Read();

                    if (reader.TokenType == JsonTokenType.EndObject) {
                        break;
                    }
                }

                var prop = string.Empty;
                if (reader.TokenType == JsonTokenType.PropertyName) {
                    prop = reader.GetString();
                }

                if (prop == "geometries") {
                    // advance to array
                    reader.Read();

                    if(reader.TokenType == JsonTokenType.StartArray) {
                        // advance to first geometry
                        reader.Read();
                    }
                }

                geometries.Add(Converter.Read(ref reader, typeof(GeometryCollection), options));
            }

            return new GeometryCollection(geometries);
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(Utf8JsonWriter writer, GeometryCollection value, JsonSerializerOptions options) {
            writer.WriteStartObject();
            writer.WriteStartArray("geometries");
            foreach (var geometry in value.Geometries) {
                Converter.Write(writer, geometry, options);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}
