// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;

namespace GeoJSON.Net.Converters {
    /// <summary>
    /// Converts <see cref="IGeometryObject"/> types to and from JSON.
    /// </summary>
    public class FeatureCollectionConverter : JsonConverter<FeatureCollection>
    {
        private static readonly FeatureFactory Factory = new FeatureFactory();
        private static readonly CrsConverter CrsConverter = new CrsConverter();
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
        public override FeatureCollection Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            var features = new List<Feature.Feature>();
            var collection = new FeatureCollection(features) {
                CRS = new UnspecifiedCRS()
            };

            if (reader.TokenType == JsonTokenType.EndObject) {
                return collection;
            }

            while(reader.Read()) {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName) {
                    var prop = reader.GetString();

                    switch (prop) {
                        case "features":
                            // advance to object
                            reader.Read();
                            while (reader.Read()) {
                                if (reader.TokenType == JsonTokenType.EndArray) {
                                    // advance to end of object
                                    reader.Read();
                                }

                                if (reader.TokenType == JsonTokenType.EndObject) {
                                    break;
                                }

                                if (reader.TokenType != JsonTokenType.StartObject) {
                                    throw new JsonException();
                                }

                                var feature = JsonSerializer.Deserialize<Feature.Feature>(ref reader, options);

                                collection.Features.Add(feature);
                            }
                            break;
                        default:
                            reader.Read();
                            continue;
                    }
                }
            }

            return collection;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(Utf8JsonWriter writer, FeatureCollection value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", "FeatureCollection");
            writer.WriteStartArray("features");
            foreach (var feat in value.Features) {
                var converter = Factory.CreateConverter(feat.GetType(), options);
                // converter.Write(writer, feat, options);
            }
            writer.WriteEndArray();

            if (value.CRS is not null) {
                CrsConverter.Write(writer, value.CRS, options);
            }

            writer.WriteEndObject();
        }
    }
}
