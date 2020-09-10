﻿// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using GeoJSON.Net.Converters;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    /// Contains an array of <see cref="Point" />.
    /// </summary>
    /// <remarks>
    /// See https://tools.ietf.org/html/rfc7946#section-3.1.3
    /// </remarks>
    [JsonConverter(typeof(MultiPointConverter))]
    public class MultiPoint : GeoJSONObject, IGeometryObject, IEqualityComparer<MultiPoint>, IEquatable<MultiPoint>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPoint" /> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public MultiPoint(IEnumerable<IPosition> positions)
        {
            Positions = new ReadOnlyCollection<IPosition>(positions?.ToArray() ?? Array.Empty<IPosition>());
        }

        [JsonConstructor]
        public MultiPoint(IEnumerable<IEnumerable<double>> coordinates)
        : this(coordinates?.Select(position => position.ToPosition())
               ?? throw new ArgumentNullException(nameof(coordinates)))
        {
            Coordinates = coordinates;
        }

        public override GeoJSONObjectType Type => GeoJSONObjectType.MultiPoint;

        public IEnumerable<IEnumerable<double>> Coordinates { get; }
        /// <summary>
        /// The points contained in this <see cref="MultiPoint"/>.
        /// </summary>
        [JsonConverter(typeof(PositionEnumerableConverter))]
        public IReadOnlyCollection<IPosition> Positions { get; }

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
                return left.Positions.SequenceEqual(right.Positions);
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
            return left != null && left.Equals(right);
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
            var hash = base.GetHashCode();
            foreach (var item in Positions)
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
