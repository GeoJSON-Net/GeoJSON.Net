// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CRSType.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the GeoJSON Coordinate Reference System Objects (CRS) types as defined in the <see cref="http://geojson.org/geojson-spec.html#coordinate-reference-system-objects">geojson.org v1.0 spec</see>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.CoordinateReferenceSystem
{
    using System;

    /// <summary>
    /// Defines the GeoJSON Coordinate Reference System Objects (CRS) types as defined in the <see cref="http://geojson.org/geojson-spec.html#coordinate-reference-system-objects">geojson.org v1.0 spec</see>.
    /// </summary>
    [Flags]
    public enum CRSType
    {
        /// <summary>
        /// Defines the <see cref="http://geojson.org/geojson-spec.html#named-crs">Named</see> CRS type.
        /// </summary>
        Name,

        /// <summary>
        /// Defines the <see cref="http://geojson.org/geojson-spec.html#linked-crs">Linked</see> CRS type.
        /// </summary>
        Link
    }
}
