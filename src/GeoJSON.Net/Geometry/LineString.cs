// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LineString.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the <see cref="http://geojson.org/geojson-spec.html#linestring">LineString</see> type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Geometry
{
    using System;
    using System.Collections.Generic;

    using GeoJSON.Net.Converters;

    using Newtonsoft.Json;

    /// <summary>
    ///   Defines the <see cref="http://geojson.org/geojson-spec.html#linestring">LineString</see> type.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class LineString : GeoJSONObject, IGeometryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LineString"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public LineString(List<IPosition> coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException("coordinates");
            }

            if (coordinates.Count < 2)
            {
                throw new ArgumentOutOfRangeException("coordinates", "According to the GeoJSON v1.0 spec a LineString must have at least two or more positions.");
            }

            this.Coordinates = coordinates;
            this.Type = GeoJSONObjectType.LineString;
        }

        /// <summary>
        /// Gets the Positions.
        /// </summary>
        /// <value>The Positions.</value>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(PositionConverter))]
        public List<IPosition> Coordinates { get; private set; }

        /// <summary>
        /// Determines whether this LineString is a <see cref="http://geojson.org/geojson-spec.html#linestring">LinearRing</see>.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if it is a linear ring; otherwise, <c>false</c>.
        /// </returns>
        public bool IsLinearRing()
        {
            return this.Coordinates.Count >= 4 && this.IsClosed();
        }

        /// <summary>
        /// Determines whether this instance has its first and last coordinate at the same position and thereby is closed.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is closed; otherwise, <c>false</c>.
        /// </returns>
        public bool IsClosed()
        {
            return this.Coordinates[0].Equals(this.Coordinates[this.Coordinates.Count - 1]);
        }
    }
}
 