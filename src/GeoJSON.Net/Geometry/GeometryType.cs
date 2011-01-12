// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeometryType.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the Geometry Object types as defined in the <see cref="http://geojson.org/geojson-spec.html#geometry-objects">geojson.org v1.0 spec</see>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net
{
    /// <summary>
    /// Defines the Geometry Object types as defined in the <see cref="http://geojson.org/geojson-spec.html#geometry-objects">geojson.org v1.0 spec</see>.
    /// </summary>
    public enum GeometryType
    {
        /// <summary>
        /// Defines the <see cref="http://geojson.org/geojson-spec.html#point">Point</see> type.
        /// </summary>
        Point,

        /// <summary>
        /// Defines the <see cref="http://geojson.org/geojson-spec.html#multipoint">MultiPoint</see> type.
        /// </summary>
        MultiPoint,

        /// <summary>
        /// Defines the <see cref="http://geojson.org/geojson-spec.html#linestring">LineString</see> type.
        /// </summary>
        LineString,

        /// <summary>
        /// Defines the <see cref="http://geojson.org/geojson-spec.html#multilinestring">MultiLineString</see> type.
        /// </summary>
        MultiLineString,

        /// <summary>
        /// Defines the <see cref="http://geojson.org/geojson-spec.html#polygon">Polygon</see> type.
        /// </summary>
        Polygon,

        /// <summary>
        /// Defines the <see cref="http://geojson.org/geojson-spec.html#multipolygon">MultiPolygon</see> type.
        /// </summary>
        MultiPolygon,

        /// <summary>
        /// Defines the <see cref="http://geojson.org/geojson-spec.html#geometrycollection">GeometryCollection</see> type.
        /// </summary>
        GeometryCollection
    }
}
