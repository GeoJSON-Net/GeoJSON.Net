// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiLineString.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the <see href="http://geojson.org/geojson-spec.html#multilinestring">MultiLineString</see> type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using GeoJSON.Net.Converters;

namespace TopoJSON.Net.Geometry
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using GeoJSON.Net.Geometry;
    using GeoJSON.Net;

    /// <summary>
    /// Defines the <see href="https://github.com/topojson/topojson-specification/blob/master/README.md#224-multilinestring">MultiLineString</see> type.
    /// </summary>
    public class TopoJSONMultiLineString : TopoJSONObject, IGeometryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiLineString"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public TopoJSONMultiLineString(List<List<int>> arcIdx)
        {
            this.ArcIdx = arcIdx;
            this.Type = GeoJSONObjectType.MultiLineString;
        }

        /// <summary>
        /// The arc indices.
        /// </summary>
        [JsonProperty(PropertyName = "arcs", Required = Required.Always)]
        public List<List<int>> ArcIdx { get; set; }

        /// <summary>
        /// Gets the Coordinates.
        /// </summary>
        /// <value>The Coordinates.</value>
        public List<TopoJSONLineString> Coordinates { get; private set; }
    }
}
