// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGeometry.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the IGeometry type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Geometry
{
    using Newtonsoft.Json;

    /// <summary>
    /// Base Interface for Geometry types.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public interface IGeometry
    {
        /// <summary>
        /// Gets the type of the Geometry object.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        GeometryType Type { get; }
    }
}
