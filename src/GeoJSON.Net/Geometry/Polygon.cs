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
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Coordinates of a Polygon are a list of LinearRing coordinate arrays.
    /// The first element in the array represents the exterior ring.
    /// Any subsequent elements represent interior rings (or holes).
    /// </summary>
    /// <seealso cref="http://geojson.org/geojson-spec.html#polygon"/>
    public class Polygon : GeoJSONObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="linearRings">The linear rings.</param>
        public Polygon(List<List<Point>> linearRings = null)
        {
            this.LinearRing = linearRings ?? new List<List<Point>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="exteriorRingPoints">The exterior ring points.</param>
        public Polygon(List<Point> exteriorRingPoints = null)
        {
            this.LinearRing = (exteriorRingPoints != null) ? new List<List<Point>> { exteriorRingPoints } : new List<List<Point>>();
        }

        /// <summary>
        /// Gets the list of points outlining this Polygon.
        /// </summary>
        public List<List<Point>> LinearRing { get; private set; }
    }
}
