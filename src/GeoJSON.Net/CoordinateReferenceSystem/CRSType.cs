// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CRSType.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
// Defines the GeoJSON Coordinate Reference System Objects (CRS) types originally defined in the <see cref="http://geojson.org/geojson-spec.html#coordinate-reference-system-objects">geojson.org v1.0 spec</see>.
// The current spec <see cref="https://tools.ietf.org/html/rfc7946#section-4" removes the CRS type, but allows to be left in for backwards compatibility.  
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace GeoJSON.Net.CoordinateReferenceSystem
{
    /// <summary>
    ///     Defines the GeoJSON Coordinate Reference System Objects (CRS) types as defined in the
    /// </summary>
    public enum CRSType
    {
        /// <summary>
        ///     Defines a CRS type where the CRS cannot be assumed
        /// </summary>
        [EnumMember(Value = "unspecified")]
        Unspecified,

        /// <summary>
        ///     Defines the Named CRS type.
        /// </summary>
        [EnumMember(Value = "name")]
        Name,

        /// <summary>
        ///     Defines the Linked CRS type.
        /// </summary>
        [EnumMember(Value = "link")]
        Link
    }
}