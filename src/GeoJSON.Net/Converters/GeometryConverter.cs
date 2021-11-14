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
    public class GeometryConverter : JsonConverter<IGeometryObject>
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
        public override IGeometryObject Read(
            ref Utf8JsonReader reader,
            Type type,
            JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;
                case JsonTokenType.StartObject:
                    return ReadGeoJson(ref reader);
            }

            throw new JsonException($"expected null, object or array token but received {reader.TokenType}");
        }

        //private static IGeometryObject[] ReadGeoJsonArray(JsonElement array)
        //{
        //    var length = array.GetArrayLength();
        //    if(length == 0)
        //        return new IGeometryObject[0];

        //    var geoJsonArray = new IGeometryObject[length];

        //    int i = 0;
        //    var enumerator = array.EnumerateArray();
        //    while(enumerator.MoveNext())
        //    {
        //        var geoJsonElement = ReadGeoJson(enumerator.Current);
        //        geoJsonArray[i] = geoJsonElement;
        //    }
        //    return geoJsonArray;
        //}

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(
            Utf8JsonWriter writer,
            IGeometryObject value,
            JsonSerializerOptions options)
        {
            // Standard serialization
            JsonSerializer.Serialize(writer, value, typeof(IGeometryObject), options);

            //writer.WriteRawValue(JsonSerializer.Serialize(value));
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
        private static IGeometryObject ReadGeoJson(ref Utf8JsonReader reader)
        {
            var document = JsonDocument.ParseValue(ref reader);
            JsonElement value = document.RootElement;
            JsonElement token;

            if (!value.TryGetProperty("type", out token))
            {
                throw new JsonException("json must contain a \"type\" property");
            }

            GeoJSONObjectType geoJsonType;

            if (!Enum.TryParse(token.GetRawText(), true, out geoJsonType))
            {
                throw new JsonException("type must be a valid geojson geometry object type");
            }

            switch (geoJsonType)
            {
                
                case GeoJSONObjectType.Point:
                    return JsonSerializer.Deserialize<Point>(ref reader);
                case GeoJSONObjectType.MultiPoint:
                    return JsonSerializer.Deserialize<MultiPoint>(ref reader);
                case GeoJSONObjectType.LineString:
                    return JsonSerializer.Deserialize<LineString>(ref reader);
                case GeoJSONObjectType.MultiLineString:
                    return JsonSerializer.Deserialize<MultiLineString>(ref reader);
                case GeoJSONObjectType.Polygon:
                    return JsonSerializer.Deserialize<Polygon>(ref reader);
                case GeoJSONObjectType.MultiPolygon:
                    return JsonSerializer.Deserialize<MultiPolygon>(ref reader);
                case GeoJSONObjectType.GeometryCollection:
                    return JsonSerializer.Deserialize<GeometryCollection>(ref reader);
                case GeoJSONObjectType.Feature:
                case GeoJSONObjectType.FeatureCollection:
                default:
                    throw new NotSupportedException("Feature and FeatureCollection types are Feature objects and not Geometry objects");
            }
        }
    }
}