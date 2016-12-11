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
using System.Collections.Generic;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    ///     In geography, a point refers to a Position on a map, expressed in latitude and longitude.
    /// </summary>
    /// <seealso cref="http://geojson.org/geojson-spec.html#point" />
    public class Point : GeoJSONObject, IGeometryObject, IEqualityComparer<Point>, IEquatable<Point>
    {
        [JsonConstructor]
        private Point()
                : base()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Point" /> class.
        /// </summary>
        /// <param name="coordinates">The Position.</param>
        public Point(IPosition coordinates)
            : this()
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
        public IPosition Coordinates { get; private set; }

        #region IEqualityComparer, IEquatable

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(this, obj as Point);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        public bool Equals(Point other)
        {
            return Equals(this, other);
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal
        /// </summary>
        public bool Equals(Point left, Point right)
        {
            if (base.Equals(left, right))
            {
                return left.Coordinates.Equals(right.Coordinates);
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal
        /// </summary>
        public static bool operator ==(Point left, Point right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }
            if (ReferenceEquals(null, right))
            {
                return false;
            }
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the specified object instances are not considered equal
        /// </summary>
        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }
        
        /// <summary>
        /// Returns the hash code for this instance
        /// </summary>
        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            hash = (hash * 397) ^ Coordinates.GetHashCode();
            return hash;
        }

        /// <summary>
        /// Returns the hash code for the specified object
        /// </summary>
        public int GetHashCode(Point other)
        {
            return other.GetHashCode();
        }

        #endregion
    }
}