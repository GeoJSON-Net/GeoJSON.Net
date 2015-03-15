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
    /// Defines the <see href="https://github.com/topojson/topojson-specification#225-polygon">Polygon</see> type.
    /// For type “Polygon”, the “arcs” member must be an array of LinearRing arc indexes. For Polygons with multiple rings, 
    /// the first must be the exterior ring and any others must be interior rings or holes. 
    /// A LinearRing is closed LineString with 4 or more positions. The first and last positions are equivalent (they represent equivalent points).
    /// </summary>
    /// <seealso href="http://geojson.org/geojson-spec.html#polygon"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class TopoJSONPolygon : TopoJSONObject, IGeometryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TopoJSONPolygon"/> class.
        /// </summary>
        /// <param name="arcIdx">The arcs index.</param>
        public TopoJSONPolygon(List<List<int>> arcIdx)
        {
            this.Type = GeoJSONObjectType.Polygon;
            this.ArcIdx = arcIdx;
            this.Coordinates = new List<GeographicPosition>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopoJSONPolygon"/> class.
        /// </summary>
        /// <param name="coords">The coordinates.</param>
        public TopoJSONPolygon(List<GeographicPosition> coords)
        {
            this.Type = GeoJSONObjectType.Polygon;
            this.Coordinates = coords;
            this.ArcIdx = new List<List<int>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopoJSONPolygon"/> class.
        /// </summary>
        public TopoJSONPolygon()
        {
            this.Type = GeoJSONObjectType.Polygon;
            this.Coordinates = new List<GeographicPosition>();
            this.ArcIdx = new List<List<int>>();
        }

        /// <summary>
        /// The arc indices.
        /// </summary>
        [JsonProperty(PropertyName = "arcs", Required = Required.Always)]
        public List<List<int>> ArcIdx { get; set; }

        /// <summary>
        /// Gets the list of points outlining this Polygon.
        /// </summary>
        public List<GeographicPosition> Coordinates { get; set; }

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
