// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGeometryObject.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the IGeometryObject type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    /// Base Interface for GeometryObject types.
    /// </summary>
    public interface IGeometryObject
    {
        /// <summary>
        /// Gets the (mandatory) type of the <see cref="http://geojson.org/geojson-spec.html#geometry-objects">GeoJSON Object</see>.
        /// However, for <see cref="http://geojson.org/geojson-spec.html#geometry-objects">GeoJSON Objects</see> only
        /// the 'Point', 'MultiPoint', 'LineString', 'MultiLineString', 'Polygon', 'MultiPolygon', or 'GeometryCollection' types are allowed.
        /// </summary>
        /// <value>
        /// The type of the object.
        /// </value>
        GeoJSONObjectType Type { get; }
    }
}
