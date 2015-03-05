// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NamedCRS.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the <see cref="http://geojson.org/geojson-spec.html#named-crs">Named CRS type</see>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.CoordinateReferenceSystem
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="http://geojson.org/geojson-spec.html#named-crs">Named CRS type</see>.
    /// </summary>
    public class NamedCRS : CRSBase, ICRSObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedCRS"/> class.
        /// </summary>
        /// <param name="name">
        /// The mandatory <see cref="http://geojson.org/geojson-spec.html#named-crs">name</see>
        /// member must be a string identifying a coordinate reference system. OGC CRS URNs such as
        /// 'urn:ogc:def:crs:OGC:1.3:CRS84' shall be preferred over legacy identifiers such as 'EPSG:4326'.
        /// </param>
        public NamedCRS(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentOutOfRangeException("name", "May not be empty");
            }

            this.Properties = new Dictionary<string, object> { { "name", name } };
            this.Type = CRSType.Name;
        }
    }
}
