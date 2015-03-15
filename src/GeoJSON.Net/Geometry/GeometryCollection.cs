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
    public class GeometryCollection : GeoJSONObject, IGeometryObject
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

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
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

            return Equals((GeometryCollection)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Geometries.GetHashCode();
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(GeometryCollection left, GeometryCollection right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(GeometryCollection left, GeometryCollection right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="GeometryCollection"/>, is equal to this instance.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        protected bool Equals(GeometryCollection other)
        {
            return base.Equals(other) && Geometries.SequenceEqual(other.Geometries);
        }
    }
}