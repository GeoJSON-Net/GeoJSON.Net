// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeographicPosition.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the Geographic Position type a.k.a. <see cref="http://geojson.org/geojson-spec.html#positions">Geographic Coordinate Reference System</see>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Geometry
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Defines the Geographic Position type a.k.a. <see cref="http://geojson.org/geojson-spec.html#positions">Geographic Coordinate Reference System</see>.
    /// </summary>
    public class GeographicPosition : Position
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicPosition"/> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="altitude">The altitude in m(eter).</param>
        public GeographicPosition(double latitude, double longitude, double? altitude = null)
            : this()
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Altitude = altitude;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicPosition"/> class.
        /// </summary>
        /// <param name="latitude">The latitude, e.g. '38.889722'.</param>
        /// <param name="longitude">The longitude, e.g. '-77.008889'.</param>
        /// <param name="altitude">The altitude in m(eter).</param>
        public GeographicPosition(string latitude, string longitude, string altitude = null)
            : this()
        {
            if (latitude == null)
            {
                throw new ArgumentNullException("latitude");
            }

            if (longitude == null)
            {
                throw new ArgumentNullException("longitude");
            }

            if (string.IsNullOrWhiteSpace(latitude))
            {
                throw new ArgumentOutOfRangeException("latitude", "May not be empty.");
            }

            if (string.IsNullOrWhiteSpace(longitude))
            {
                throw new ArgumentOutOfRangeException("longitude", "May not be empty.");
            }

            double lat;
            double lng;

            if (!double.TryParse(latitude, NumberStyles.Float, CultureInfo.InvariantCulture, out lat))
            {
                throw new ArgumentOutOfRangeException("latitude", "Latitude must be a proper lat (+/- double) value, e.g. '38.889722'.");
            }

            if (!double.TryParse(longitude, NumberStyles.Float, CultureInfo.InvariantCulture, out lng))
            {
                throw new ArgumentOutOfRangeException("longitude", "Longitude must be a proper lon (+/- double) value, e.g. '-77.008889'.");
            }

            this.Latitude = lat;
            this.Longitude = lng;

            if (altitude == null)
            {
                this.Altitude = null;
            }
            else
            {
                double alt;
                if (!double.TryParse(altitude, NumberStyles.Float, CultureInfo.InvariantCulture, out alt))
                {
                    throw new ArgumentOutOfRangeException("altitude", "Altitude must be a proper altitude (m(eter) as double) value, e.g. '6500'.");
                }

                this.Altitude = alt;
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="GeographicPosition"/> class from being created.
        /// </summary>
        private GeographicPosition()
        {
            this.Coordinates = new double?[3];
        }

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude
        {
            get
            {
                return this.Coordinates[0].GetValueOrDefault();
            }

            private set
            {
                this.Coordinates[0] = value;
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
                return this.Coordinates[1].GetValueOrDefault();
            }

            private set
            {
                this.Coordinates[1] = value;
            }
        }

        /// <summary>
        /// Gets the altitude.
        /// </summary>
        public double? Altitude
        {
            get
            {
                return this.Coordinates[2];
            }

            private set
            {
                this.Coordinates[2] = value;
            }
        }

        /// <summary>
        /// Gets or sets the coordinates, is a 2-size array
        /// </summary>
        /// <value>
        /// The coordinates.
        /// </value>
        private double?[] Coordinates { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Altitude == null ? string.Format(CultureInfo.InvariantCulture, "Latitude: {0}, Longitude: {1}", this.Latitude, this.Longitude) : string.Format(CultureInfo.InvariantCulture, "Latitude: {0}, Longitude: {1}, Altitude: {2}", this.Latitude, this.Longitude, this.Altitude);
        }
    }
}
