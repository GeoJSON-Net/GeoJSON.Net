// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeometryCollection.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the <see cref="http://geojson.org/geojson-spec.html#geometry-collection">GeometryCollection</see> type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Converters;
using Newtonsoft.Json;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    ///     Defines the <see cref="http://geojson.org/geojson-spec.html#geometry-collection">GeometryCollection</see> type.
    /// </summary>
    public class GeometryCollection : GeoJSONObject, IGeometryObject, IEqualityComparer<GeometryCollection>, IEquatable<GeometryCollection>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GeometryCollection" /> class.
        /// </summary>
        public GeometryCollection() : this(new List<IGeometryObject>())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GeometryCollection" /> class.
        /// </summary>
        /// <param name="geometries">The geometries contained in this GeometryCollection.</param>
        public GeometryCollection(List<IGeometryObject> geometries)
            : base()
        {
            if (geometries == null)
            {
                throw new ArgumentNullException("geometries");
            }

            Geometries = geometries;
            Type = GeoJSONObjectType.GeometryCollection;
        }

        /// <summary>
        ///     Gets the list of Polygons enclosed in this MultiPolygon.
        /// </summary>
        [JsonProperty(PropertyName = "geometries", Required = Required.Always)]
        [JsonConverter(typeof(GeometryConverter))]
        public List<IGeometryObject> Geometries { get; private set; }

        #region IEqualityComparer, IEquatable

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(this, obj as GeometryCollection);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        public bool Equals(GeometryCollection other)
        {
            return Equals(this, other);
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal
        /// </summary>
        public bool Equals(GeometryCollection left, GeometryCollection right)
        {
            if (base.Equals(left, right))
            {
                return left.Geometries.SequenceEqual(right.Geometries);
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal
        /// </summary>
        public static bool operator ==(GeometryCollection left, GeometryCollection right)
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
        public static bool operator !=(GeometryCollection left, GeometryCollection right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Returns the hash code for this instance
        /// </summary>
        public override int GetHashCode()
        {
            int hash = base.GetHashCode();
            foreach (var item in Geometries)
            {
                hash = (hash * 397) ^ item.GetHashCode();
            }
            return hash;
        }

        /// <summary>
        /// Returns the hash code for the specified object
        /// </summary>
        public int GetHashCode(GeometryCollection other)
        {
            return other.GetHashCode();
        }

        #endregion
    }
}