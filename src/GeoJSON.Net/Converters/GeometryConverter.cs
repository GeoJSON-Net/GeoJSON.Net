﻿// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using GeoJSON.Net.Geometry;

namespace GeoJSON.Net.Converters {
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
        public override IGeometryObject Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            IGeometryObject geometry = null;
            GeoJSONObjectType? geoJsonType = null;
            ReadOnlySpan<byte> coordinateSpan = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return geometry;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    continue;
                }

                var propertyName = reader.GetString();

                if (propertyName == "type")
                {
                    reader.Read();

                    var geometryType = reader.GetString();

                    if (!Enum.TryParse(geometryType, ignoreCase: false, out GeoJSONObjectType geoJsonGeometryType) &&
                        !Enum.TryParse(geometryType, ignoreCase: true, out geoJsonGeometryType))
                    {
                        throw new JsonException("json must contain a \"type\" property that is a valid geojson geometry object type");
                    }

                    geoJsonType = geoJsonGeometryType;
                }

                if (propertyName == "coordinates" && geoJsonType.HasValue)
                {
                    // advance to coordinates
                    reader.Read();

                    switch (geoJsonType.Value)
                    {
                        case GeoJSONObjectType.Point:
                            geometry = new Point(new PositionConverter().Read(ref reader, typeof(IPosition), options));
                            break;
                        case GeoJSONObjectType.MultiPoint:
                            geometry = new MultiPoint(new PositionEnumerableConverter().Read(ref reader, typeof(IEnumerable<IPosition>), options));
                            break;
                        case GeoJSONObjectType.LineString:
                            geometry = new LineString(new PositionEnumerableConverter().Read(ref reader, typeof(IEnumerable<IPosition>), options));
                            break;
                        case GeoJSONObjectType.MultiLineString:
                            geometry = new MultiLineString(new LineStringEnumerableConverter().Read(ref reader, typeof(IEnumerable<LineString>), options));
                            break;
                        case GeoJSONObjectType.Polygon:
                            geometry = new PolygonConverter().Read(ref reader, typeof(IEnumerable<IPosition>), options);
                            break;
                        case GeoJSONObjectType.MultiPolygon:
                            geometry = new MultiPolygon(new PolygonEnumerableConverter().Read(ref reader, typeof(IEnumerable<Polygon>), options));
                            break;
                        case GeoJSONObjectType.GeometryCollection:
                            geometry = new GeometryCollectionConverter().Read(ref reader, typeof(GeometryCollection), options);
                            break;
                    }
                }
                else if (!coordinateSpan.IsEmpty && geoJsonType.HasValue)
                {
                    var newReader = new Utf8JsonReader(coordinateSpan);
                    switch (geoJsonType.Value)
                    {
                        case GeoJSONObjectType.Point:
                            geometry = (IGeometryObject)new PositionConverter().Read(ref newReader, typeof(IPosition), options);
                            break;
                        case GeoJSONObjectType.MultiPoint:
                            geometry = new MultiPoint(new PositionEnumerableConverter().Read(ref newReader, typeof(IEnumerable<IPosition>), options));
                            break;
                        case GeoJSONObjectType.LineString:
                            geometry = new LineStringConverter().Read(ref newReader, typeof(IEnumerable<IPosition>), options);
                            break;
                        case GeoJSONObjectType.MultiLineString:
                            geometry = new MultiLineString(new LineStringEnumerableConverter().Read(ref newReader, typeof(IEnumerable<LineString>), options));
                            break;
                        case GeoJSONObjectType.Polygon:
                            geometry = new PolygonConverter().Read(ref newReader, typeof(IEnumerable<IPosition>), options);
                            break;
                        case GeoJSONObjectType.MultiPolygon:
                            geometry = new MultiPolygon(new PolygonEnumerableConverter().Read(ref newReader, typeof(IEnumerable<Polygon>), options));
                            break;
                        case GeoJSONObjectType.GeometryCollection:
                            geometry = new GeometryCollectionConverter().Read(ref newReader, typeof(GeometryCollection), options);
                            break;
                    }
                }
                else if (propertyName == "coordinates" && geometry is null)
                {
                    reader.Read();
                    coordinateSpan = reader.ValueSpan;
                }
            }

            if (geometry is null)
            {
                throw new JsonException("expected null, object or array token but received " + reader.TokenType);
            }

            return geometry;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(Utf8JsonWriter writer, IGeometryObject item, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            writer.WriteStringValue(item.Type.ToString());
            writer.WritePropertyName("coordinates");

            switch(item)
            {
                case Point point:
                {
                    new PositionConverter().Write(writer, point.Coordinates, options);
                    break;
                }
                case MultiPoint multiPoint:
                {
                    new PositionEnumerableConverter().Write(writer, multiPoint.Positions, options);
                    break;

                }
                case LineString line:
                {
                    new PositionEnumerableConverter().Write(writer, line.Positions, options);
                    break;
                }
                case MultiLineString multiLine:
                {
                    new LineStringEnumerableConverter().Write(writer, multiLine.LineStrings, options);
                    break;
                }
                case Polygon polygon:
                {
                    new PolygonConverter().Write(writer, polygon, options);
                    break;
                }
                case MultiPolygon multiPolygon:
                {
                    new PolygonEnumerableConverter().Write(writer, multiPolygon.Polygons, options);
                    break;
                }
                case GeometryCollection collection:
                {
                    new GeometryCollectionConverter().Write(writer, collection, options);
                    break;
                }
                default:
                    throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
            }

            writer.WriteEndObject();
        }
    }
}
