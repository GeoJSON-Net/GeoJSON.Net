// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGeoJSONObject.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the IGeoJSONObject interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net
{
    /// <summary>
    /// Base Interface for GeoJSONObject types.
    /// </summary>
    public interface IGeoJSONObject
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>
        /// The type of the object.
        /// </value>
        GeoJSONObjectType Type { get; }
    }
}
