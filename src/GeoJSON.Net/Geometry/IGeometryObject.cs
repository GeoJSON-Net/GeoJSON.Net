// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using GeoJSON.Net.Converters;
using System.Text.Json.Serialization;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    /// Base Interface for GeometryObject types.
    /// </summary>
    //[JsonConverter(typeof(GeometryConverter))]
    public interface IGeometryObject
    {
        /// <summary>
        /// Gets the (mandatory) type of the GeoJSON Object.
        /// However, for GeoJSON Objects only the 'Point', 'MultiPoint', 'LineString', 'MultiLineString',
        /// 'Polygon', 'MultiPolygon', or 'GeometryCollection' types are allowed.
        /// </summary>
        /// <remarks>
        /// See https://tools.ietf.org/html/rfc7946#section-3.1
        /// </remarks>
        /// <value>
        /// The type of the object.
        /// </value>
        [JsonPropertyName("type")]
        //, Required = Required.Always, DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        GeoJSONObjectType Type { get; }
    }
}
