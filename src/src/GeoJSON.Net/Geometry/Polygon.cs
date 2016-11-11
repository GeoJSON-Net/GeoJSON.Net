// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Polygon.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the <see cref="http://geojson.org/geojson-spec.html#polygon">Polygon</see> type.
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
    ///     Defines the <see cref="http://geojson.org/geojson-spec.html#polygon">Polygon</see> type.
    ///     Coordinates of a Polygon are a list of
    ///     <see cref="http://geojson.org/geojson-spec.html#linestring">linear rings</see>
    ///     coordinate arrays. The first element in the array represents the exterior ring. Any subsequent elements
    ///     represent interior rings (or holes).
    /// </summary>
    /// <seealso cref="http://geojson.org/geojson-spec.html#polygon" />
    public class Polygon : GeoJSONObject, IGeometryObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Polygon" /> class.
        /// </summary>
        /// <param name="coordinates">
        ///     The <see cref="http://geojson.org/geojson-spec.html#linestring">linear rings</see> with the first element
        ///     in the array representing the exterior ring. Any subsequent elements represent interior rings (or holes).
        /// </param>
        public Polygon(List<LineString> coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException("coordinates");
            }

            if (coordinates.Any(linearRing => !linearRing.IsLinearRing()))
            {
                throw new ArgumentException("All elements must be closed LineStrings with 4 or more positions" +
                                            " (see GeoJSON spec at 'http://geojson.org/geojson-spec.html#linestring').", "coordinates");
            }

            Coordinates = coordinates;
            Type = GeoJSONObjectType.Polygon;
        }

        /// <summary>
        ///     Gets the list of points outlining this Polygon.
        /// </summary>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(PolygonConverter))]
        public List<LineString> Coordinates { get; set; }

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

            return Equals((Polygon)obj);
        }

        public override int GetHashCode()
        {
            return Coordinates.GetHashCode();
        }

        public static bool operator ==(Polygon left, Polygon right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Polygon left, Polygon right)
        {
            return !Equals(left, right);
        }

        protected bool Equals(Polygon other)
        {
            return base.Equals(other) && Coordinates.SequenceEqual(other.Coordinates);
        }
    }
}