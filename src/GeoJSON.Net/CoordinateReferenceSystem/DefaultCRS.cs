// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeometryCollection.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
// Defines the GeoJSON Coordinate Reference System Objects (CRS) types originally defined in the <see cref="http://geojson.org/geojson-spec.html#coordinate-reference-system-objects">geojson.org v1.0 spec</see>.
// The current spec <see cref="https://tools.ietf.org/html/rfc7946#section-4" removes the CRS type, but allows to be left in for backwards compatibility.  
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.CoordinateReferenceSystem
{
    /// <summary>
    ///     The default CRS is a geographic coordinate reference system,
    ///     using the WGS84 datum, and with longitude and latitude units of decimal degrees.
    ///     see https://tools.ietf.org/html/rfc7946#section-4
    /// </summary>
    public class DefaultCRS : NamedCRS
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultCRS" /> class.
        /// </summary>
        private DefaultCRS()
            : base("urn:ogc:def:crs:OGC::CRS84")
        {
        }

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <value>
        ///     The instance.
        /// </value>
        public static DefaultCRS Instance { get; } = new DefaultCRS();
    }
}