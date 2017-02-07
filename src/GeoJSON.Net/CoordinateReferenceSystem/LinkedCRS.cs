// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkedCRS.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
// Defines the <see cref="http://geojson.org/geojson-spec.html#named-crs">Linked CRS type</see>.
// The current spec <see cref="https://tools.ietf.org/html/rfc7946#section-4" removes the CRS type, but allows to be left in for backwards compatibility. 
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
        ///     The mandatory href member must be a dereferenceable URI.
        /// </param>
        /// <param name="type">
        ///     The optional type member will be put in the properties Dictionary
        /// </param>
        public LinkedCRS(string href, string type = "")
        {
            if (href == null)
            {
                throw new ArgumentNullException(nameof(href));
            }

            Uri uri;
            if (href.Length == 0 || !Uri.TryCreate(href, UriKind.RelativeOrAbsolute, out uri))
            {
                throw new ArgumentException("must be a dereferenceable URI", nameof(href));
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
        ///     The mandatory href member must be a dereferenceable URI.
        /// </param>
        /// <param name="type">
        ///     The optional type member will be put in the properties Dictionary
        /// </param>
        public LinkedCRS(Uri href, string type = "") : this(href != null ? href.ToString() : null, type)
        {
        }
    }
}