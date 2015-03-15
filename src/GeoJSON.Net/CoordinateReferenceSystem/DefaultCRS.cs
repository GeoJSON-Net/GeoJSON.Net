// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeometryCollection.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the <see cref="http://geojson.org/geojson-spec.html#coordinate-reference-system-objects">default CRS</see> type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.CoordinateReferenceSystem
{
    /// <summary>
    ///     The default CRS is a geographic coordinate reference system,
    ///     using the WGS84 datum, and with longitude and latitude units of decimal degrees.
    ///     see http://geojson.org/geojson-spec.html#coordinate-reference-system-objects
    /// </summary>
    public class DefaultCRS : NamedCRS
    {
        /// <summary>
        ///     The CRS
        /// </summary>
        private static readonly DefaultCRS Crs = new DefaultCRS();

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
        public static DefaultCRS Instance
        {
            get { return Crs; }
        }
    }
}