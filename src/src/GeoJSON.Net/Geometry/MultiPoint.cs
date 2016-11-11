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

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    ///     Contains an array of <see cref="Point" />s.
    /// </summary>
    /// <seealso cref="http://geojson.org/geojson-spec.html#multipoint" />
    public class MultiPoint : GeoJSONObject, IGeometryObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiPoint" /> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public MultiPoint(List<Point> coordinates = null)
        {
            this.Coordinates = coordinates ?? new List<Point>();
            this.Type = GeoJSONObjectType.MultiPoint;
        }

        /// <summary>
        ///     Gets the Coordinates.
        /// </summary>
        /// <value>The Coordinates.</value>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(MultiPointConverter))]
        public List<Point> Coordinates { get; private set; }

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
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((MultiPoint)obj);
        }

        public override int GetHashCode()
        {
            return Coordinates.GetHashCode();
        }

        public static bool operator ==(MultiPoint left, MultiPoint right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MultiPoint left, MultiPoint right)
        {
            return !Equals(left, right);
        }

        protected bool Equals(MultiPoint other)
        {
            return base.Equals(other) && Coordinates.SequenceEqual(other.Coordinates);
        }
    }
}