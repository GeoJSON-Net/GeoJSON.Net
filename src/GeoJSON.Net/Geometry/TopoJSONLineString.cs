// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LineString.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the <see href="http://geojson.org/geojson-spec.html#linestring">LineString</see> type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;

namespace TopoJSON.Net.Geometry
{
    using System;
    using System.Collections.Generic;

    using GeoJSON.Net.Converters;

    using Newtonsoft.Json;
    using GeoJSON.Net;
    using GeoJSON.Net.Geometry;

    /// <summary>
    ///   Defines the <see href="http://geojson.org/geojson-spec.html#linestring">LineString</see> type.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class TopoJSONLineString : TopoJSONObject, IGeometryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TopoJSONLineString"/> class.
        /// </summary>
        /// <param name="arcindex">The arcs index.</param>
        public TopoJSONLineString(List<int> arcindex)
        {
            this.ArcIdx = arcindex;
            this.Coordinates = new List<GeographicPosition>();
            Type = GeoJSONObjectType.LineString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopoJSONLineString"/> class.
        /// </summary>
        /// <param name="coords">A list of coordinates.</param>
        public TopoJSONLineString(List<GeographicPosition> coords)
        {
            this.ArcIdx = new List<int>();
            this.Coordinates = coords;
            Type = GeoJSONObjectType.LineString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TopoJSONLineString"/> class.
        /// </summary>
        public TopoJSONLineString()
        {
            this.ArcIdx = new List<int>();
            this.Coordinates = new List<GeographicPosition>();
            Type = GeoJSONObjectType.LineString;
        }

        /// <summary>
        /// The arc indices.
        /// </summary>
        [JsonProperty(PropertyName = "arcs", Required = Required.Always)]
        public List<int> ArcIdx { get; set; }

        /// <summary>
        /// Gets the Positions.
        /// </summary>
        /// <value>The Positions.</value>
        public List<GeographicPosition> Coordinates { get; set; }

        /// <summary>
        /// Determines whether this LineString is a <see href="http://geojson.org/geojson-spec.html#linestring">LinearRing</see>.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if it is a linear ring; otherwise, <c>false</c>.
        /// </returns>
        public bool IsLinearRing()
        {
            return Coordinates.Count >= 4 && IsClosed();
        }

        /// <summary>
        /// Determines whether this instance has its first and last coordinate at the same position and thereby is closed.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is closed; otherwise, <c>false</c>.
        /// </returns>
        public bool IsClosed()
        {
            if (Coordinates[0] is GeographicPosition) 
            {
                var firstCoordinate = Coordinates[0] as GeographicPosition;
                var lastCoordinate = Coordinates[Coordinates.Count - 1] as GeographicPosition;

                return firstCoordinate.Latitude == lastCoordinate.Latitude
                    && firstCoordinate.Longitude == lastCoordinate.Longitude
                    && firstCoordinate.Altitude == lastCoordinate.Altitude;
            }
            else
                return Coordinates[0].Equals(Coordinates[Coordinates.Count - 1]);
        }
    }
}
 