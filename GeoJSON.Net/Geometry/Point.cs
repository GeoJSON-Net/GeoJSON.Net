// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Point.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the Point type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net
{
    using System;

    using GeoJSON.Net.Converters;
    using GeoJSON.Net.Geometry;

    using Newtonsoft.Json;

    /// <summary>
    /// In geography, a point refers to a Position on a map, often expressed in latitude and longitude.
    /// </summary>
    /// <example>
    /// For example, the point 37° 46' 21.7776", -122° 24' 20.4366" refers to SimpleGeo's San Francisco office.
    /// </example>
    /// <seealso cref="http://geojson.org/geojson-spec.html#point"/>
    [JsonObject(MemberSerialization.OptIn)]
    public class Point : IGeometry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        /// <param name="position">The Position.</param>
        public Point(Position position)
            : this()
        {
            if (position == null)
            {
                throw new ArgumentNullException("position");
            }

            this.Position = position;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Point"/> class from being created.
        /// </summary>
        private Point()
        {
            this.Type = GeometryType.Point;
        }
        
        /// <summary>
        /// Gets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude
        {
            get
            {
                return this.Position.Latitude;
            }
        }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude
        {
            get
            {
                return this.Position.Longitude;
            }
        }

        /// <summary>
        /// Gets the type of the Geometry object.
        /// </summary>
        public GeometryType Type { get; private set; }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        /// <value>The Position.</value>
        [JsonProperty(PropertyName = "coordinates")]
        [JsonConverter(typeof(PositionConverter))]
        private Position Position { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Position.ToString();
        }
    }
}
