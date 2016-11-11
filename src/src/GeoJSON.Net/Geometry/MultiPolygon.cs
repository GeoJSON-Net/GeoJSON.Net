// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiPolygon.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the <see cref="http://geojson.org/geojson-spec.html#multipolygon">MultiPolygon</see> type.
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
    ///     Defines the <see cref="http://geojson.org/geojson-spec.html#multipolygon">MultiPolygon</see> type.
    /// </summary>
    public class MultiPolygon : GeoJSONObject, IGeometryObject
    {
        public MultiPolygon() : this(new List<Polygon>())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiPolygon" /> class.
        /// </summary>
        /// <param name="polygons">The polygons contained in this MultiPolygon.</param>
        public MultiPolygon(List<Polygon> polygons)
        {
            if (polygons == null)
            {
                throw new ArgumentNullException("polygons");
            }

            Coordinates = polygons;
            Type = GeoJSONObjectType.MultiPolygon;
        }

        /// <summary>
        ///     Gets the list of Polygons enclosed in this MultiPolygon.
        /// </summary>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(MultiPolygonConverter))]
        public List<Polygon> Coordinates { get; private set; }

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

            return Equals((MultiPolygon)obj);
        }

        public override int GetHashCode()
        {
            return Coordinates.GetHashCode();
        }

        public static bool operator ==(MultiPolygon left, MultiPolygon right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MultiPolygon left, MultiPolygon right)
        {
            return !Equals(left, right);
        }

        protected bool Equals(MultiPolygon other)
        {
            return base.Equals(other) && Coordinates.SequenceEqual(other.Coordinates);
        }
    }
}