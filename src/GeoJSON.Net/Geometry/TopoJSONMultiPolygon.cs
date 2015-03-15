namespace TopoJSON.Net.Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using Newtonsoft.Json;
    using GeoJSON.Net.Converters;
    using GeoJSON.Net.Geometry;
    using GeoJSON.Net;

    /// <summary>
    /// Defines the <see href="https://github.com/topojson/topojson-specification#226-multipolygon">MultiPolygon</see> type.
    /// For type “MultiPolygon”, the “arcs” member must be an array of Polygon arc indexes.
    /// </summary>
    /// <seealso href="http://geojson.org/geojson-spec.html#multipolygon"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class TopoJSONMultiPolygon : TopoJSONObject, IGeometryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TopoJSONPolygon"/> class.
        /// </summary>
        public TopoJSONMultiPolygon(List<List<List<int>>> arcIdx)
        {
            this.Type = GeoJSONObjectType.MultiPolygon;
            this.Coordinates = new List<TopoJSONPolygon>();
            this.ArcIdx = arcIdx;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopoJSONPolygon"/> class.
        /// </summary>
        public TopoJSONMultiPolygon()
        {
            this.Type = GeoJSONObjectType.MultiPolygon;
            this.Coordinates = new List<TopoJSONPolygon>();
            this.ArcIdx = new List<List<List<int>>>();
        }

        /// <summary>
        /// The arc indices.
        /// </summary>
        [JsonProperty(PropertyName = "arcs", Required = Required.Always)]
        public List<List<List<int>>> ArcIdx { get; set; }

        /// <summary>
        /// Gets the list of points outlining this Polygon.
        /// </summary>
        public List<TopoJSONPolygon> Coordinates { get; set; }

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return Coordinates.GetHashCode();
        }
    }
}
