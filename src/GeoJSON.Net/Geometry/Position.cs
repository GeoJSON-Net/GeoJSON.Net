// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System;
using System.Collections.Generic;
using System.Globalization;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    /// A position is the fundamental geometry construct, consisting of <see cref="Latitude" />,
    /// <see cref="Longitude" /> and (optionally) <see cref="Altitude" />.
    /// </summary>
    public class Position : IPosition, IEqualityComparer<Position>, IEquatable<Position>
    {
        private static readonly DoubleTenDecimalPlaceComparer DoubleComparer = new DoubleTenDecimalPlaceComparer();

        /// <summary>
        /// Initializes a new instance of the <see cref="Position" /> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="altitude">The altitude in m(eter).</param>
        public Position(double latitude, double longitude, double? altitude = null)
        {
            if (Math.Abs(latitude) > 90)
            {
                throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be a proper lat (+/- double) value between -90 and 90.");
            }

            if (Math.Abs(longitude) > 180)
            {
                throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be a proper lon (+/- double) value between -180 and 180.");
            }

            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Position" /> class.
        /// </summary>
        /// <param name="latitude">The latitude, e.g. '38.889722'.</param>
        /// <param name="longitude">The longitude, e.g. '-77.008889'.</param>
        /// <param name="altitude">The altitude in m(eters).</param>
        public Position(string latitude, string longitude, string altitude = null)
        {
            if (string.IsNullOrEmpty(latitude))
            {
                throw new ArgumentOutOfRangeException(nameof(latitude), "May not be empty.");
            }

            if (string.IsNullOrEmpty(longitude))
            {
                throw new ArgumentOutOfRangeException(nameof(longitude), "May not be empty.");
            }

            if (!double.TryParse(latitude, NumberStyles.Float, CultureInfo.InvariantCulture, out double lat) || Math.Abs(lat) > 90)
            {
                throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be a proper lat (+/- double) value between -90 and 90.");
            }

            if (!double.TryParse(longitude, NumberStyles.Float, CultureInfo.InvariantCulture, out double lon) || Math.Abs(lon) > 180)
            {
                throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be a proper lon (+/- double) value between -180 and 180.");
            }

            Latitude = lat;
            Longitude = lon;

            if (altitude != null)
            {
                if (!double.TryParse(altitude, NumberStyles.Float, CultureInfo.InvariantCulture, out double alt))
                {
                    throw new ArgumentOutOfRangeException(nameof(altitude), "Altitude must be a proper altitude (m(eter) as double) value, e.g. '6500'.");
                }

                Altitude = alt;
            }
        }
        
        /// <summary>
        /// Gets the altitude.
        /// </summary>
        public double? Altitude { get; }

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        public double Longitude { get; }
        
        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
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
            return (this == (obj as Position));
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        public bool Equals(Position other)
        {
            return (this == other);
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal
        /// </summary>
        public bool Equals(Position left, Position right)
        {
            return (left == right);
        }


        /// <summary>
        /// Determines whether the specified object instances are considered equal
        /// </summary>
        public static bool operator ==(Position left, Position right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }
            if (ReferenceEquals(null, right) || ReferenceEquals(null, left))
            {
                return false;
            }
            if (!DoubleComparer.Equals(left.Latitude, right.Latitude) ||
                !DoubleComparer.Equals(left.Longitude, right.Longitude))
            {
                return false;
            }
            return left.Altitude.HasValue == right.Altitude.HasValue &&
                   (!left.Altitude.HasValue || DoubleComparer.Equals(left.Altitude.Value, right.Altitude.Value));
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal
        /// </summary>
        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Returns the hash code for this instance
        /// </summary>
        public override int GetHashCode()
        {
            var hash = 397 ^ Latitude.GetHashCode();
            hash = (hash * 397) ^ Longitude.GetHashCode();
            hash = (hash * 397) ^ Altitude.GetValueOrDefault().GetHashCode();
            return hash;
        }

        /// <summary>
        /// Returns the hash code for the specified object
        /// </summary>
        public int GetHashCode(Position other)
        {
            return other.GetHashCode();
        }

        #endregion
    }
}