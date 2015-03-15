// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Point.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the Point type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using GeoJSON.Net.Converters;
using Newtonsoft.Json;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    ///     In geography, a point refers to a Position on a map, expressed in latitude and longitude.
    /// </summary>
    /// <seealso cref="http://geojson.org/geojson-spec.html#point" />
    public class Point : GeoJSONObject, IGeometryObject
    {
        [JsonConstructor]
        private Point() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Point" /> class.
        /// </summary>
        /// <param name="coordinates">The Position.</param>
        public Point(IPosition coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException("coordinates");
            }

            Coordinates = coordinates;
            Type = GeoJSONObjectType.Point;
        }

        /// <summary>
        ///     Gets or sets the Coordinate(s).
        /// </summary>
        /// <value>The Coordinates.</value>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(PointConverter))]
        public IPosition Coordinates { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            return Coordinates.GetHashCode();
        }

        public static bool operator ==(Point left, Point right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !Equals(left, right);
        }

        protected bool Equals(Point other)
        {
            return base.Equals(other) && Coordinates.Equals(other.Coordinates);
        }
    }
}