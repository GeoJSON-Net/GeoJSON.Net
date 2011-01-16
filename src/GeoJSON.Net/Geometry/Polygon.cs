// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Polygon.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the Polygon type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GeoJSON.Net.Converters;

    using Newtonsoft.Json;

    /// <summary>
    /// Coordinates of a Polygon are a list of <see cref="http://geojson.org/geojson-spec.html#id16">linear rings</see>
    /// coordinate arrays. The first element in the array represents the exterior ring. Any subsequent elements
    /// represent interior rings (or holes).
    /// </summary>
    /// <seealso cref="http://geojson.org/geojson-spec.html#polygon"/>
    public class Polygon : GeoJSONObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="linearRings">
        /// The <see cref="http://geojson.org/geojson-spec.html#id16">linear rings</see> with the first element
        /// in the array representing the exterior ring. Any subsequent elements represent interior rings (or holes).
        /// </param>
        public Polygon(List<LineString> linearRings = null)
        {
            if (linearRings == null)
            {
                throw new ArgumentNullException("linearRings");
            }

            if (linearRings.Any(linearRing => !linearRing.IsLinearRing()))
            {
                throw new ArgumentOutOfRangeException("linearRings", "All elements must be closed LineStrings with 4 or more positions (see GeoJSON spec at 'http://geojson.org/geojson-spec.html#linestring').");
            }

            this.Coordinates = linearRings;
            this.Type = GeoJSONObjectType.Polygon;
        }

        /// <summary>
        /// Gets the list of points outlining this Polygon.
        /// </summary>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        public List<LineString> Coordinates { get; private set; }
    }
}
