// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiPoint.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the MultiPoint type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Converters;
using Newtonsoft.Json;
using System;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    ///     Contains an array of <see cref="Point" />s.
    /// </summary>
    /// <seealso cref="https://tools.ietf.org/html/rfc7946#section-3.1.3" />
    public class MultiPoint : GeoJSONObject, IGeometryObject, IEqualityComparer<MultiPoint>, IEquatable<MultiPoint>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiPoint" /> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public MultiPoint(List<Point> coordinates = null)
            : base()
        {
            Coordinates = coordinates ?? new List<Point>();
            Type = GeoJSONObjectType.MultiPoint;
        }

        /// <summary>
        ///     Gets the Coordinates.
        /// </summary>
        /// <value>The Coordinates.</value>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(MultiPointConverter))]
        public List<Point> Coordinates { get; private set; }

        #region IEqualityComparer, IEquatable

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(this, obj as MultiPoint);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        public bool Equals(MultiPoint other)
        {
            return Equals(this, other);
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal
        /// </summary>
        public bool Equals(MultiPoint left, MultiPoint right)
        {
            if (base.Equals(left, right))
            {
                return left.Coordinates.SequenceEqual(right.Coordinates);
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal
        /// </summary>
        public static bool operator ==(MultiPoint left, MultiPoint right)
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
        public static bool operator !=(MultiPoint left, MultiPoint right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Returns the hash code for this instance
        /// </summary>
        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            foreach (var item in Coordinates)
            {
                hash = (hash * 397) ^ item.GetHashCode();
            }
            return hash;
        }

        /// <summary>
        /// Returns the hash code for the specified object
        /// </summary>
        public int GetHashCode(MultiPoint other)
        {
            return other.GetHashCode();
        }

        #endregion
    }
}