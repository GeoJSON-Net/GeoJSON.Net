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
    /// The Features endpoint provides details for all SimpleGeo features.
    /// </summary>
    /// <seealso cref="http://simplegeo.com/docs/getting-started/simplegeo-101#feature"/>
    /// <seealso cref="http://simplegeo.com/docs/api-endpoints/simplegeo-features"/>
    /// <seealso cref="http://developers.simplegeo.com/blog/2010/12/08/simplegeo-features-api/"/>
    public class Feature : GeoJSONObject
    {
        /// <summary>
        /// Gets the <see cref="id"/>.
        /// </summary>
        /// <value>The handle.</value>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; private set; }

        /// <summary>
        /// Gets the geometry.
        /// </summary>
        /// <value>The geometry.</value>
        [JsonProperty(PropertyName = "geometry", Required = Required.AllowNull)]
        [JsonConverter(typeof(GeometryConverter))]
        public IGeometryObject GeometryObject { get; private set; }
        
        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>The properties.</value>
        [JsonProperty(PropertyName = "properties", Required = Required.AllowNull)]
        public Dictionary<string, object> Properties { get; private set; }
    }
}
