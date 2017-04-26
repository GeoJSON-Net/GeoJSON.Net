// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeographicPosition.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the Geographic Position type GeographicPosition.
//   See https://tools.ietf.org/html/rfc7946#section-3.1.1
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    /// Defines the Geographic Position type.
    /// </summary>
    /// <remarks>
    /// See https://tools.ietf.org/html/rfc7946#section-3.1.1
    /// </remarks>
    [Obsolete("This class is obsolete, please use Position")]
    public class GeographicPosition : Position
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicPosition" /> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="altitude">The altitude in m(eter).</param>
        public GeographicPosition(double latitude, double longitude, double? altitude = null) 
            : base(latitude, longitude, altitude)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeographicPosition" /> class.
        /// </summary>
        /// <param name="latitude">The latitude, e.g. '38.889722'.</param>
        /// <param name="longitude">The longitude, e.g. '-77.008889'.</param>
        /// <param name="altitude">The altitude in m(eters).</param>
        public GeographicPosition(string latitude, string longitude, string altitude = null) 
            : base(latitude, longitude, altitude)
        {
        }
    }
}