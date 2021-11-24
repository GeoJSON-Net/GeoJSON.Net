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
    public class GeometryConverter : JsonConverter<object>
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
            return typeof(IGeometryObject).IsAssignableFromType(objectType);
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
        public override object Read(
            ref Utf8JsonReader reader,
            Type type,
            JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;
                case JsonTokenType.StartObject:
                    return ReadGeoJson(ref reader, options);
            }

            throw new JsonException($"expected null, object or array token but received {reader.TokenType}");
        }

        /// <summary>
        /// Reads the geo json.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="Newtonsoft.Json.JsonReaderException">
        /// json must contain a "type" property
        /// or
        /// type must be a valid geojson geometry object type
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// Feature and FeatureCollection types are Feature objects and not Geometry objects
        /// </exception>
        private static object ReadGeoJson(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            var document = JsonDocument.ParseValue(ref reader);
            JsonElement value = document.RootElement;
            JsonElement token;

            if (!value.TryGetProperty("type", out token))
            {
                throw new JsonException("json must contain a \"type\" property");
            }

            GeoJSONObjectType geoJsonType;

            if (!Enum.TryParse(token.GetString(), true, out geoJsonType))
            {
                throw new JsonException("type must be a valid geojson geometry object type");
            }

            switch (geoJsonType)
            {

                case GeoJSONObjectType.Point:
                    return value.Deserialize<Point>(options);
                case GeoJSONObjectType.MultiPoint:
                    return value.Deserialize<MultiPoint>(options);
                case GeoJSONObjectType.LineString:
                    return value.Deserialize<LineString>(options);
                case GeoJSONObjectType.MultiLineString:
                    return value.Deserialize<MultiLineString>(options);
                case GeoJSONObjectType.Polygon:
                    return value.Deserialize<Polygon>(options);
                case GeoJSONObjectType.MultiPolygon:
                    return value.Deserialize<MultiPolygon>(options);
                case GeoJSONObjectType.GeometryCollection:
                    return value.Deserialize<GeometryCollection>(options);
                case GeoJSONObjectType.Feature:
                case GeoJSONObjectType.FeatureCollection:
                default:
                    throw new NotSupportedException("Feature and FeatureCollection types are Feature objects and not Geometry objects");
            }
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(
            Utf8JsonWriter writer,
            object value,
            JsonSerializerOptions options)
        {
            var castedValue = value as IGeometryObject;
            // Standard serialization
            switch (castedValue.Type)
            {
                case GeoJSONObjectType.Point:
                    JsonSerializer.Serialize<Point>(writer, (Point)value);
                    break;
                case GeoJSONObjectType.MultiPoint:
                    JsonSerializer.Serialize<MultiPoint>(writer, (MultiPoint)value);
                    break;
                case GeoJSONObjectType.LineString:
                    JsonSerializer.Serialize<LineString>(writer, (LineString)value);
                    break;
                case GeoJSONObjectType.MultiLineString:
                    JsonSerializer.Serialize<MultiLineString>(writer, (MultiLineString)value);
                    break;
                case GeoJSONObjectType.Polygon:
                    JsonSerializer.Serialize<Polygon>(writer, (Polygon)value);
                    break;
                case GeoJSONObjectType.MultiPolygon:
                    JsonSerializer.Serialize<MultiPolygon>(writer, (MultiPolygon)value);
                    break;
                case GeoJSONObjectType.GeometryCollection:
                    JsonSerializer.Serialize<GeometryCollection>(writer, (GeometryCollection)value);
                    break;
                case GeoJSONObjectType.Feature:
                case GeoJSONObjectType.FeatureCollection:
                default:
                    throw new NotSupportedException("Feature and FeatureCollection types are Feature objects and not Geometry objects");
            }
        }

    }
}