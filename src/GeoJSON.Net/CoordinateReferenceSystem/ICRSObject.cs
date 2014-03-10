// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICRSObject.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Base Interface for CRSBase Object types.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.CoordinateReferenceSystem
{
    /// <summary>
    /// Base Interface for CRSBase Object types.
    /// </summary>
    public interface ICRSObject
    {
        /// <summary>
        /// Gets the CRS type.
        /// </summary>
        CRSType Type { get; }
    }
}
