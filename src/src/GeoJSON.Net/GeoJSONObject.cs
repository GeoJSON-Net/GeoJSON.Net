// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeoJSONObject.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the GeoJSONObject type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Runtime.Serialization;
using GeoJSON.Net.Converters;
using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GeoJSON.Net
{
    /// <summary>
    ///     Base class for all IGeometryObject implementing types
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class GeoJSONObject : IGeoJSONObject
    {
        internal static readonly DoubleTenDecimalPlaceComparer DoubleComparer = new DoubleTenDecimalPlaceComparer();

        protected GeoJSONObject()
        {
            CRS = DefaultCRS.Instance;
        }

        /// <summary>
        ///     Gets or sets the (optional)
        ///     <see cref="http://geojson.org/geojson-spec.html#coordinate-reference-system-objects">Bounding Boxes</see>.
        /// </summary>
        /// <value>
        ///     The value of <see cref="BoundingBoxes" /> must be a 2*n array where n is the number of dimensions represented in
        ///     the
        ///     contained geometries, with the lowest values for all axes followed by the highest values.
        ///     The axes order of a bbox follows the axes order of geometries.
        ///     In addition, the coordinate reference system for the bbox is assumed to match the coordinate reference
        ///     system of the GeoJSON object of which it is a member.
        /// </value>
        [JsonProperty(PropertyName = "bbox", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double[] BoundingBoxes { get; set; }

        /// <summary>
        ///     Gets or sets the (optional)
        ///     <see cref="http://geojson.org/geojson-spec.html#coordinate-reference-system-objects">
        ///         Coordinate Reference System
        ///         Object.
        ///     </see>
        /// </summary>
        /// <value>
        ///     The Coordinate Reference System Objects.
        /// </value>
        [JsonProperty(PropertyName = "crs", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
            NullValueHandling = NullValueHandling.Include)]
        [JsonConverter(typeof(CrsConverter))]
        //[DefaultValue(typeof(DefaultCRS), "")]
        public ICRSObject CRS { get; set; }

        /// <summary>
        ///     Gets the (mandatory) type of the
        ///     <see cref="http://geojson.org/geojson-spec.html#geojson-objects">GeoJSON Object</see>.
        /// </summary>
        /// <value>
        ///     The type of the object.
        /// </value>
        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public GeoJSONObjectType Type { get; internal set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
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

            return Equals((GeoJSONObject)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (BoundingBoxes != null ? BoundingBoxes.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CRS != null ? CRS.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)Type;
                return hashCode;
            }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(GeoJSONObject left, GeoJSONObject right)
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
        public static bool operator !=(GeoJSONObject left, GeoJSONObject right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="GeoJSONObject" />, is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="GeoJSONObject" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="GeoJSONObject" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool Equals(GeoJSONObject other)
        {
            if (Type != other.Type)
            {
                return false;
            }

            if (!Equals(CRS, other.CRS))
            {
                return false;
            }

            if (BoundingBoxes == null && other.BoundingBoxes == null)
            {
                return true;
            }

            if ((BoundingBoxes != null && other.BoundingBoxes == null) ||
                (BoundingBoxes == null && other.BoundingBoxes != null))
            {
                return false;
            }

            return BoundingBoxes.SequenceEqual(other.BoundingBoxes, DoubleComparer);
        }

        /// <summary>
        ///     Called when [deserialized].
        /// </summary>
        /// <param name="streamingContext">The streaming context.</param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext streamingContext)
        {
            if (CRS == null)
            {
                CRS = DefaultCRS.Instance;
            }
        }

        /// <summary>
        ///     Called when [serialized].
        /// </summary>
        /// <param name="streamingContext">The streaming context.</param>
        [OnSerialized]
        private void OnSerialized(StreamingContext streamingContext)
        {
            if (CRS == null)
            {
                CRS = DefaultCRS.Instance;
            }
        }

        /// <summary>
        ///     Called when [serializing].
        /// </summary>
        /// <param name="streamingContext">The streaming context.</param>
        [OnSerializing]
        private void OnSerializing(StreamingContext streamingContext)
        {
            if (CRS is DefaultCRS)
            {
                CRS = null;
            }
        }
    }
}