// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeoJSONObjectType.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the GeoJSON Objects types as defined in the <see cref="https://tools.ietf.org/html/rfc7946#section-3">RFC 7946</see>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net
{
    /// <summary>
    /// Defines the GeoJSON Objects types as defined in the <see cref="https://tools.ietf.org/html/rfc7946#section-3">RFC 7946</see>.
    /// </summary>
    public enum GeoJSONObjectType
    {
        /// <summary>
        /// Defines the <see cref="https://tools.ietf.org/html/rfc7946#section-3.1.2">Point</see> type.
        /// </summary>
        Point,

        /// <summary>
        /// Defines the <see cref="https://tools.ietf.org/html/rfc7946#section-3.1.3">MultiPoint</see> type.
        /// </summary>
        MultiPoint,

        /// <summary>
        /// Defines the <see cref="https://tools.ietf.org/html/rfc7946#section-3.1.4">LineString</see> type.
        /// </summary>
        LineString,

        /// <summary>
        /// Defines the <see chref="https://tools.ietf.org/html/rfc7946#section-3.1.5">MultiLineString</see> type.
        /// </summary>
        MultiLineString,

        /// <summary>
        /// Defines the <see cref="https://tools.ietf.org/html/rfc7946#section-3.1.6">Polygon</see> type.
        /// </summary>
        Polygon,

        /// <summary>
        /// Defines the <see cref="https://tools.ietf.org/html/rfc7946#section-3.1.7">MultiPolygon</see> type.
        /// </summary>
        MultiPolygon,

        /// <summary>
        /// Defines the <see cref="https://tools.ietf.org/html/rfc7946#section-3.1.8">GeometryCollection</see> type.
        /// </summary>
        GeometryCollection,

        /// <summary>
        /// Defines the <see cref="https://tools.ietf.org/html/rfc7946#section-3.2">Feature</see> type.
        /// </summary>
        Feature,

        /// <summary>
        /// Defines the <see cref="https://tools.ietf.org/html/rfc7946#section-3.3">FeatureCollection</see> type.
        /// </summary>
        FeatureCollection
    }
}
