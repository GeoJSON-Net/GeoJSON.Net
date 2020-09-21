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
    public class FeatureConverter : JsonConverter<Feature.Feature> {
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
        public override Feature.Feature Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options) {
            IGeometryObject geometry = null;
            var properties = new Dictionary<string, object>();
            var id = string.Empty;
            if (reader.TokenType == JsonTokenType.EndObject) {
                return new Feature.Feature(geometry, id);
            }

            while (reader.Read()) {
                if (reader.TokenType == JsonTokenType.EndObject) {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName) {
                    var prop = reader.GetString();

                    switch (prop) {
                        case "id":
                            // advance to property
                            reader.Read();
                            id = reader.GetString();
                            break;
                        case "geometry":
                            // advance to array
                            reader.Read();

                            geometry = Converter.Read(ref reader, typeof(Feature.Feature), options);
                            break;
                        case "properties":
                            // advance to object
                            reader.Read();
                            while (reader.Read()) {
                                if (reader.TokenType == JsonTokenType.EndObject) {
                                    break;
                                }

                                if (reader.TokenType != JsonTokenType.PropertyName) {
                                    throw new JsonException();
                                }

                                var key = reader.GetString();
                                var value = JsonSerializer.Deserialize<object>(ref reader, options);

                                properties.Add(key, value);
                            }
                            break;
                        default:
                            reader.Read();
                            continue;
                    }
                }
            }

            return new Feature.Feature(geometry, properties, id);
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(Utf8JsonWriter writer, Feature.Feature value, JsonSerializerOptions options) {
            writer.WriteStartObject();
            writer.WriteString("type", "Feature");

            if (!string.IsNullOrEmpty(value.Id)) {
                writer.WriteString("id", value.Id);
            }

            writer.WritePropertyName("geometry");
            Converter.Write(writer, value.Geometry, options);

            writer.WritePropertyName("properties");
            JsonSerializer.Serialize(writer, value.Properties, options);

            writer.WriteEndObject();
        }
    }
}
