// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Position.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the Position type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Defines the Position type.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public Position(double latitude, double longitude)
            : this()
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/> class using decimal degree string lat/lng values.
        /// </summary>
        /// <param name="latitude">The latitude, e.g. '38.889722'.</param>
        /// <param name="longitude">The longitude, e.g. '-77.008889'.</param>
        public Position(string latitude, string longitude)
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
                throw new ArgumentOutOfRangeException("latitude", "May not be empty.");
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
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Position"/> class from being created.
        /// </summary>
        private Position()
        {
            this.Coordinates = new double[2];
        }

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude
        {
            get
            {
                return this.Coordinates[0];
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
                return this.Coordinates[1];
            }

            private set
            {
                this.Coordinates[1] = value;
            }
        }

        /// <summary>
        /// Gets or sets the coordinates, is a 2-size array
        /// </summary>
        /// <value>
        /// The coordinates.
        /// </value>
        private double[] Coordinates { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Latitude: {0}, Longitude: {1}", this.Latitude, this.Longitude);
        }
    }
}
