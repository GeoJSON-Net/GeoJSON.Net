// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feature.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the Feature type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GeoJSON.Net.Converters;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;

namespace GeoJSON.Net.Feature
{
    /// <summary>
    ///     A GeoJSON <see cref="http://geojson.org/geojson-spec.html#feature-objects">Feature Object</see>.
    /// </summary>
    public class Feature : GeoJSONObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Feature" /> class.
        /// </summary>
        /// <param name="geometry">The Geometry Object.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="id">The (optional) identifier.</param>
        [JsonConstructor]
        public Feature(IGeometryObject geometry, Dictionary<string, object> properties = null, string id = null)
        {
            Geometry = geometry;
            Properties = properties ?? new Dictionary<string, object>();
            Id = id;

            Type = GeoJSONObjectType.Feature;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Feature" /> class.
        /// </summary>
        /// <param name="geometry">The Geometry Object.</param>
        /// <param name="properties">
        ///     Class used to fill feature properties. Any public member will be added to feature
        ///     properties
        /// </param>
        /// <param name="id">The (optional) identifier.</param>
        public Feature(IGeometryObject geometry, object properties, string id = null)
        {
            Geometry = geometry;
            Id = id;

            if (properties == null)
            {
                Properties = new Dictionary<string, object>();
            }
            else
            {
                Properties = properties.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(properties, null));
            }

            Type = GeoJSONObjectType.Feature;
        }

        /// <summary>
        ///     Gets or sets the geometry.
        /// </summary>
        /// <value>
        ///     The geometry.
        /// </value>
        [JsonProperty(PropertyName = "geometry", Required = Required.AllowNull)]
        [JsonConverter(typeof(GeometryConverter))]
        public IGeometryObject Geometry { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        /// <value>The handle.</value>
        [JsonProperty(PropertyName = "id", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        ///     Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        [JsonProperty(PropertyName = "properties", Required = Required.AllowNull)]
        public Dictionary<string, object> Properties { get; private set; }
    }
}