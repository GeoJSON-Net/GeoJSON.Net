﻿// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    /// Converts <see cref="IGeoJSONObject"/> types to and from JSON.
    /// </summary>
    public class GeoJsonConverter : JsonConverter<IGeoJSONObject>
    {
        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(IGeoJSONObject).IsAssignableFromType(objectType);
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
        public override IGeoJSONObject Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            // switch (reader.TokenType)
            // {
            //     case JsonToken.Null:
            //         return null;
            //     case JsonToken.StartObject:
            //         var value = JObject.Load(reader);
            //         return ReadGeoJson(value);
            //     case JsonToken.StartArray:
            //         var values = JArray.Load(reader);
            //         var geometries = new List<IGeoJSONObject>(values.Count);
            //         geometries.AddRange(values.Cast<JObject>().Select(ReadGeoJson));
            //         return geometries;
            // }

            throw new JsonException("expected null, object or array token but received " + reader.TokenType);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(Utf8JsonWriter writer, IGeoJSONObject value, JsonSerializerOptions options)
        {
            // serializer.Serialize(writer, value);
        }

        /// <summary>
        /// Reads the geo json.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="Newtonsoft.Json.JsonReaderException">
        /// json must contain a "type" property
        /// or
        /// type must be a valid geojson object type
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// Unknown geoJsonType {geoJsonType}
        /// </exception>
        // private static IGeoJSONObject ReadGeoJson(JObject value)
        // {
        //     JToken token;

        //     if (!value.TryGetValue("type", StringComparison.OrdinalIgnoreCase, out token))
        //     {
        //         throw new JsonReaderException("json must contain a \"type\" property");
        //     }

        //     GeoJSONObjectType geoJsonType;
        //     if (!Enum.TryParse(token.Value<string>(), true, out geoJsonType))
        //     {
        //         throw new JsonReaderException("type must be a valid geojson object type");
        //     }

        //     switch (geoJsonType)
        //     {
        //         case GeoJSONObjectType.Point:
        //             return value.ToObject<Point>();
        //         case GeoJSONObjectType.MultiPoint:
        //             return value.ToObject<MultiPoint>();
        //         case GeoJSONObjectType.LineString:
        //             return value.ToObject<LineString>();
        //         case GeoJSONObjectType.MultiLineString:
        //             return value.ToObject<MultiLineString>();
        //         case GeoJSONObjectType.Polygon:
        //             return value.ToObject<Polygon>();
        //         case GeoJSONObjectType.MultiPolygon:
        //             return value.ToObject<MultiPolygon>();
        //         case GeoJSONObjectType.GeometryCollection:
        //             return value.ToObject<GeometryCollection>();
        //         case GeoJSONObjectType.Feature:
        //             return value.ToObject<Feature.Feature>();
        //         case GeoJSONObjectType.FeatureCollection:
        //             return value.ToObject<FeatureCollection>();
        //         default:
        //             throw new NotSupportedException($"Unknown geoJsonType {geoJsonType}");
        //     }
        // }
    }
}
