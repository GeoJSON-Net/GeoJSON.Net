// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeographicPosition.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the Geographic Position type <see cref="https://tools.ietf.org/html/rfc7946#section-3.1.1">GeographicPosition</see>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    ///     Defines the Geographic Position type
    ///     <see cref="https://tools.ietf.org/html/rfc7946#section-3.1.1">Position</see>.
    /// </summary>
    public class GeographicPosition : Position, IEqualityComparer<GeographicPosition>, IEquatable<GeographicPosition>
    {
        private static readonly NullableDoubleTenDecimalPlaceComparer DoubleComparer = new NullableDoubleTenDecimalPlaceComparer();

        /// <summary>
        ///     Initializes a new instance of the <see cref="GeographicPosition" /> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="altitude">The altitude in m(eter).</param>
        public GeographicPosition(double latitude, double longitude, double? altitude = null)
            : this()
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GeographicPosition" /> class.
        /// </summary>
        /// <param name="latitude">The latitude, e.g. '38.889722'.</param>
        /// <param name="longitude">The longitude, e.g. '-77.008889'.</param>
        /// <param name="altitude">The altitude in m(eters).</param>
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
            double lon;

            if (!double.TryParse(latitude, NumberStyles.Float, CultureInfo.InvariantCulture, out lat) || Math.Abs(lat) > 90)
            {
                throw new ArgumentOutOfRangeException("latitude", "Latitude must be a proper lat (+/- double) value between -90 and 90.");
            }

            if (!double.TryParse(longitude, NumberStyles.Float, CultureInfo.InvariantCulture, out lon) || Math.Abs(lon) > 180)
            {
                throw new ArgumentOutOfRangeException("longitude", "Longitude must be a proper lon (+/- double) value between -180 and 180.");
            }

            Latitude = lat;
            Longitude = lon;

            if (altitude != null)
            {
                double alt;
                if (!double.TryParse(altitude, NumberStyles.Float, CultureInfo.InvariantCulture, out alt))
                {
                    throw new ArgumentOutOfRangeException("altitude", "Altitude must be a proper altitude (m(eter) as double) value, e.g. '6500'.");
                }

                Altitude = alt;
            }
        }

        /// <summary>
        ///     Prevents a default instance of the <see cref="GeographicPosition" /> class from being created.
        /// </summary>
        private GeographicPosition()
            : base()
        {
            Coordinates = new double?[3];
        }

        /// <summary>
        ///     Gets the altitude.
        /// </summary>
        public double? Altitude
        {
            get { return Coordinates[2]; }
            private set { Coordinates[2] = value; }
        }

        /// <summary>
        ///     Gets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude
        {
            get { return Coordinates[0].GetValueOrDefault(); }
            private set { Coordinates[0] = value; }
        }

        /// <summary>
        ///     Gets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude
        {
            get { return Coordinates[1].GetValueOrDefault(); }
            private set { Coordinates[1] = value; }
        }

        /// <summary>
        ///     Gets or sets the coordinates, is a 2-size array
        /// </summary>
        /// <value>
        ///     The coordinates.
        /// </value>
        private double?[] Coordinates { get; set; }
        
        /// <summary>
        ///     Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Altitude == null
                ? string.Format(CultureInfo.InvariantCulture, "Latitude: {0}, Longitude: {1}", Latitude, Longitude)
                : string.Format(CultureInfo.InvariantCulture, "Latitude: {0}, Longitude: {1}, Altitude: {2}", Latitude, Longitude, Altitude);
        }

        #region IEqualityComparer, IEquatable

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        public override bool Equals(object obj)
        {
            return (this == (obj as GeographicPosition));
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        public bool Equals(GeographicPosition other)
        {
            return (this == other);
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal
        /// </summary>
        public bool Equals(GeographicPosition left, GeographicPosition right)
        {
            return (left == right);
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal
        /// </summary>
        public static bool operator ==(GeographicPosition left, GeographicPosition right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }
            if (ReferenceEquals(null, right))
            {
                return false;
            }
            return left.Coordinates.SequenceEqual(right.Coordinates, DoubleComparer);
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal
        /// </summary>
        public static bool operator !=(GeographicPosition left, GeographicPosition right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Returns the hash code for this instance
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 1;
            foreach (var item in Coordinates)
            {
                hash = (hash * 397) ^ item.GetHashCode();
            }
            return hash;
        }

        /// <summary>
        /// Returns the hash code for the specified object
        /// </summary>
        public int GetHashCode(GeographicPosition other)
        {
            return other.GetHashCode();
        }

        #endregion

    }
}