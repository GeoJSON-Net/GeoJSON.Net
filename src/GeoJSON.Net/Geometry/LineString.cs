// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LineString.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the <see cref="http://geojson.org/geojson-spec.html#linestring">LineString</see> type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;

namespace GeoJSON.Net.Geometry
{
    using System;
    using System.Collections.Generic;

    using Converters;

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
        public LineString(IEnumerable<IPosition> coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException("coordinates");
            }

            var coordsList = coordinates.ToList();

            if (coordsList.Count < 2)
            {
                throw new ArgumentOutOfRangeException("coordinates", "According to the GeoJSON v1.0 spec a LineString must have at least two or more positions.");
            }

            Coordinates = coordsList;
            Type = GeoJSONObjectType.LineString;
        }

        /// <summary>
        /// Gets the Positions.
        /// </summary>
        /// <value>The Positions.</value>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(LineStringConverter))]
        public List<IPosition> Coordinates { get; private set; }

        /// <summary>
        /// Determines whether this LineString is a <see cref="http://geojson.org/geojson-spec.html#linestring">LinearRing</see>.
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
 