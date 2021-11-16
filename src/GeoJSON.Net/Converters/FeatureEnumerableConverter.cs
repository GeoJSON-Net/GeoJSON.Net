// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using GeoJSON.Net.Geometry;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    /// Converts <see cref="IGeometryObject"/> types to and from JSON.
    /// </summary>
    public class FeatureEnumerableConverter : JsonConverter<List<Feature.Feature>>
    {
        private static readonly FeatureConverter FeatureConverter = new FeatureConverter();
        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(List<Feature.Feature>).IsAssignableFrom(objectType);
        }

        public override List<Feature.Feature> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;
                case JsonTokenType.StartArray:
                    break;
            }

            var startDepth = reader.CurrentDepth;
            var result = new List<Feature.Feature>();
            while (reader.Read())
            {
                if (JsonTokenType.EndArray == reader.TokenType && reader.CurrentDepth == startDepth)
                {
                    return new List<Feature.Feature>(result);
                }
                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    result.Add((Feature.Feature)FeatureConverter.Read(
                        ref reader,
                        typeof(Feature.Feature),
                        options));
                }
            }

            throw new JsonException($"expected null, object or array token but received {reader.TokenType}");
        }

        public override void Write(Utf8JsonWriter writer, List<Feature.Feature> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach(var feature in value)
            {
                FeatureConverter.Write(writer, feature, options);
            }
            writer.WriteEndArray();
        }
    }
}