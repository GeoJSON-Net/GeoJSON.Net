// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CRSBase.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the CRSBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json.Converters;

namespace GeoJSON.Net.CoordinateReferenceSystem
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Base class for all IGeometryObject implementing types
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class CRSBase
    {
        /// <summary>
        /// Gets the type of the GeometryObject object.
        /// </summary>
        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public CRSType Type { get; internal set; }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        [JsonProperty(PropertyName = "properties", Required = Required.Always)]
        public Dictionary<string, object> Properties { get; internal set; }
    }
}
