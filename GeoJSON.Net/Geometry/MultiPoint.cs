// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiPoint.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the MultiPoint type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net
{
    using System;
    using System.Collections.Generic;

    using GeoJSON.Net.Converters;
    using GeoJSON.Net.Geometry;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains an array of <see cref="Point"/>s.
    /// </summary>
    /// <seealso cref="http://geojson.org/geojson-spec.html#multipoint"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class MultiPoint : IGeometry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPoint"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public MultiPoint(List<Point> points)
            : this()
        {
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }

            this.Points = points;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="MultiPoint"/> class from being created.
        /// </summary>
        private MultiPoint()
        {
            this.Points = new List<Point>();
            this.Type = GeometryType.MultiPoint;
        }
        
        /// <summary>
        /// Gets the type of the Geometry object.
        /// </summary>
        public GeometryType Type { get; private set; }

        /// <summary>
        /// Gets the Points.
        /// </summary>
        /// <value>The Points.</value>
        [JsonProperty(PropertyName = "coordinates")]
        [JsonConverter(typeof(PointsConverter))]
        public List<Point> Points { get; private set; }
    }
}
