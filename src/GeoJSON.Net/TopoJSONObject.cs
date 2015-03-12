// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeoJSONObject.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the GeoJSONObject type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace TopoJSON.Net
{
    using GeoJSON.Net.Converters;
    using Newtonsoft.Json.Converters;
    using GeoJSON.Net.Geometry;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using GeoJSON.Net;

    /// <summary>
    /// Base class for all IGeometryObject implementing types
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class TopoJSONObject : ITopoJSONObject
    {
        /// <summary>
        /// Gets the (mandatory) type of the <see href="https://github.com/topojson/topojson-specification/blob/master/README.md#21-topology-objects">TopoJSON Object</see>.
        /// </summary>
        /// <value>
        /// The type of the object.
        /// </value>
        [JsonProperty(PropertyName = "type", Required = Required.Always, Order = 1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public GeoJSONObjectType Type { get; internal set; }

        #region ---------- Bounding Boxes ----------
        /// <summary>
        /// Gets or sets the (optional) <see href="https://github.com/topojson/topojson-specification/blob/master/README.md#3-bounding-boxes">Bounding Boxes</see>.
        /// </summary>
        /// <value>
        /// The value of the bbox member must be a 2*n array where n is the number of dimensions represented in the
        /// contained geometries, with the lowest values for all axes followed by the highest values.
        /// The axes order of a bbox follows the axes order of geometries. The bounding box should not be transformed using the topology’s transform, if any.
        /// </value>
        [JsonProperty(PropertyName = "bbox", Required = Required.Default)]
        public double[] BoundingBoxes { get; set; }
        #endregion

        #region ---------- Properties ----------
        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        [JsonProperty(PropertyName = "properties", Required = Required.Default)]
        public Dictionary<string, object> Properties { get; private set; }
        #endregion

        #region ---------- Name ----------
        /// <summary>
        /// A unique Id. This is needed for named (sub-) objects
        /// </summary>
        public string name { get; set; }
        #endregion
    }

}
