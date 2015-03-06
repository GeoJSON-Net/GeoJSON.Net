// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Polygon.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the <see cref="http://geojson.org/geojson-spec.html#polygon">Polygon</see> type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
    /// Defines the <see href="http://geojson.org/geojson-spec.html#polygon">Polygon</see> type.
    /// Coordinates of a Polygon are a list of <see href="http://geojson.org/geojson-spec.html#linestring">linear rings</see>
    /// coordinate arrays. The first element in the array represents the exterior ring. Any subsequent elements
    /// represent interior rings (or holes).
    /// </summary>
    /// <seealso href="http://geojson.org/geojson-spec.html#polygon"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class TopoJSONPolygon : TopoJSONObject, IGeometryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TopoJSONPolygon"/> class.
        /// </summary>
        public TopoJSONPolygon(List<List<int>> arcIdx)
        {
            this.Type = GeoJSONObjectType.Polygon;
            this.ArcIdx = arcIdx;
        }

        /// <summary>
        /// The arc indices.
        /// </summary>
        [JsonProperty(PropertyName = "arcs", Required = Required.Always)]
        //[JsonConverter(typeof(PolygonArcsConverter))]
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
