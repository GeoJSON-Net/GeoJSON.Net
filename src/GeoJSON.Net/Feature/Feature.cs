// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feature.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the Feature type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Feature
{
    using System.Collections.Generic;

    using GeoJSON.Net.Converters;
    using GeoJSON.Net.Geometry;

    using Newtonsoft.Json;

    /// <summary>
    /// A GeoJSON <see cref="http://geojson.org/geojson-spec.html#feature-objects">Feature Object</see>.
    /// </summary>
    public class Feature : GeoJSONObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Feature"/> class.
        /// </summary>
        /// <param name="geometry">The Geometry Object.</param>
        /// <param name="properties">The properties.</param>
        public Feature(IGeometryObject geometry, Dictionary<string, object> properties = null)
        {
            this.Geometry = geometry;
            this.Properties = properties;

            this.Type = GeoJSONObjectType.Feature;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The handle.</value>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the geometry.
        /// </summary>
        /// <value>
        /// The geometry.
        /// </value>
        [JsonProperty(PropertyName = "geometry", Required = Required.AllowNull)]
        [JsonConverter(typeof(GeometryConverter))]
        public IGeometryObject Geometry { get; set; }
        
        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        [JsonProperty(PropertyName = "properties", Required = Required.AllowNull)]
        public Dictionary<string, object> Properties { get; private set; }
    }
}
