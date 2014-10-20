// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Polygon.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the <see cref="http://geojson.org/geojson-spec.html#polygon">Polygon</see> type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using Newtonsoft.Json;
    using GeoJSON.Net.Converters;

    /// <summary>
    /// Defines the <see cref="http://geojson.org/geojson-spec.html#polygon">Polygon</see> type.
    /// Coordinates of a Polygon are a list of <see cref="http://geojson.org/geojson-spec.html#linestring">linear rings</see>
    /// coordinate arrays. The first element in the array represents the exterior ring. Any subsequent elements
    /// represent interior rings (or holes).
    /// </summary>
    /// <seealso cref="http://geojson.org/geojson-spec.html#polygon"/>
    public class Polygon : GeoJSONObject, IGeometryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="coordinates">
        /// The <see cref="http://geojson.org/geojson-spec.html#linestring">linear rings</see> with the first element
        /// in the array representing the exterior ring. Any subsequent elements represent interior rings (or holes).
        /// </param>
        public Polygon(List<LineString> coordinates = null)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException("linearRings");
            }
            if (coordinates.Any(linearRing => !linearRing.IsLinearRing()))
            {
                throw new ArgumentOutOfRangeException("linearRings", "All elements must be closed LineStrings with 4 or more positions (see GeoJSON spec at 'http://geojson.org/geojson-spec.html#linestring').");
            }

            this.Coordinates = coordinates;
            this.Type = GeoJSONObjectType.Polygon;
        }
        /// <summary>
        /// Returns a pretty string object of the linestring
        /// </summary>
        public string customString()
        {
            string ret = "[";
            bool first = true;
            for (int i = 0; i < Coordinates.Count(); i++)
            {
                if (!first)
                {
                    ret += ",";
                }
                first = false;
                ret += Coordinates[i].customString();
            }
            ret += "]";

            return ret;
        }

        /// <summary>
        /// Gets the list of points outlining this Polygon.
        /// </summary>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(PolygonConverter))]
        public List<LineString> Coordinates { get; set; }


        public static bool operator ==(Polygon a, Polygon b)
        {
            if ((object)a == null && (object)b == null)
                return true;

            if ((object)a != null && (object)b == null || (object)a == null && (object)b != null)
                return false;

            if (a.Coordinates == null && b.Coordinates == null)
                return true;

            //If only one of the coordinates is null, or their count is different
            if (a.Coordinates == null || b.Coordinates == null || a.Coordinates.Count != b.Coordinates.Count)
                return false;

            if (a.Coordinates.Count == 0)
                return true;

            if (a.Coordinates[0].Coordinates.Count != b.Coordinates[0].Coordinates.Count)
                return false;

            //logic for multipolygon to compare each line string            
            for (int i = 0; i < a.Coordinates.Count; i++) 
            { 
                for (int j = 0; j < a.Coordinates[i].Coordinates.Count; j++)
                {
                    var firstComparer = a.Coordinates[i].Coordinates[j] as GeographicPosition;
                    var secondComparer = b.Coordinates[i].Coordinates[j] as GeographicPosition;
                    
                    if (Math.Abs(firstComparer.Latitude - secondComparer.Latitude) > 0.0001
                        || Math.Abs(firstComparer.Longitude - secondComparer.Longitude) > 0.0001
                        || (firstComparer.Altitude.HasValue && secondComparer.Altitude.HasValue && (Math.Abs(firstComparer.Altitude.Value - secondComparer.Altitude.Value) > 0.0001)))   
                        return false;
                }
            }
            return true;
        }

        public static bool operator !=(Polygon a, Polygon b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Coordinates.GetHashCode();
        }
    }
}
