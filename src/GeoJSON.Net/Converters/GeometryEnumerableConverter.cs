// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using GeoJSON.Net.Geometry;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    /// Converts <see cref="IGeometryObject"/> types to and from JSON.
    /// </summary>
    public class GeometryEnumerableConverter : JsonConverter<ReadOnlyCollection<IGeometryObject>>
    {
        private static readonly GeometryConverter GeometryConverter = new GeometryConverter();

        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(ReadOnlyCollection<IGeometryObject>).IsAssignableFromType(objectType);
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
        public override ReadOnlyCollection<IGeometryObject> Read(
            ref Utf8JsonReader reader,
            Type type,
            JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;
                case JsonTokenType.StartArray:
                    break;
            }

            var startDepth = reader.CurrentDepth;
            var result = new List<IGeometryObject>();
            while (reader.Read())
            {
                if (JsonTokenType.EndArray == reader.TokenType && reader.CurrentDepth == startDepth)
                {
                    return new ReadOnlyCollection<IGeometryObject>(result);
                }
                if (reader.TokenType == JsonTokenType.StartArray)
                {
                    result.Add(GeometryConverter.Read(
                        ref reader,
                        typeof(IEnumerable<IPosition>),
                        options));
                }
            }

            throw new JsonException($"expected null, object or array token but received {reader.TokenType}");
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(
            Utf8JsonWriter writer,
            ReadOnlyCollection<IGeometryObject> value,
            JsonSerializerOptions options)
        {
            
            foreach(var item in value)
                GeometryConverter.Write(writer, item, options);
            // Standard serialization
            //writer.WriteStartObject();
            //writer.WriteNumber("Miyagi", 10);
            //writer.WriteEndObject();
        }
    }
}