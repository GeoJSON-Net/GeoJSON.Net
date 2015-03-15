// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkedCRS.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the <see cref="http://geojson.org/geojson-spec.html#named-crs">Linked CRS type</see>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace GeoJSON.Net.CoordinateReferenceSystem
{
    /// <summary>
    ///     Defines the <see cref="http://geojson.org/geojson-spec.html#linked-crs">Linked CRS type</see>.
    /// </summary>
    public class LinkedCRS : CRSBase, ICRSObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LinkedCRS" /> class.
        /// </summary>
        /// <param name="href">
        ///     The mandatory <see cref="http://geojson.org/geojson-spec.html#linked-crs">href</see> member must be
        ///     a dereferenceable URI.
        /// </param>
        /// <param name="type">
        ///     The optional type member will be put in the properties Dictionary as specified in the
        ///     <see cref="http://geojson.org/geojson-spec.html#linked-crs">GeoJSON spec</see>.
        /// </param>
        public LinkedCRS(string href, string type = "")
        {
            if (href == null)
            {
                throw new ArgumentNullException("href");
            }

            Uri uri;
            if (href.Length == 0 || !Uri.TryCreate(href, UriKind.RelativeOrAbsolute, out uri))
            {
                throw new ArgumentException("must be a dereferenceable URI", "href");
            }

            Properties = new Dictionary<string, object> { { "href", href } };

            if (!string.IsNullOrWhiteSpace(type))
            {
                Properties.Add("type", type);
            }

            Type = CRSType.Link;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LinkedCRS" /> class.
        /// </summary>
        /// <param name="href">
        ///     The mandatory <see cref="http://geojson.org/geojson-spec.html#linked-crs">href</see> member must be
        ///     a dereferenceable URI.
        /// </param>
        /// <param name="type">
        ///     The optional type member will be put in the properties Dictionary as specified in the
        ///     <see cref="http://geojson.org/geojson-spec.html#linked-crs">GeoJSON spec</see>.
        /// </param>
        public LinkedCRS(Uri href, string type = "") : this(href != null ? href.ToString() : null, type)
        {
        }
    }
}