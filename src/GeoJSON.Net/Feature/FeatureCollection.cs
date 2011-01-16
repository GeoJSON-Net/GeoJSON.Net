// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeatureCollection.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the FeatureCollection type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Feature
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines the FeatureCollection type.
    /// </summary>
    public class FeatureCollection : GeoJSONObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureCollection"/> class.
        /// </summary>
        /// <param name="features">The features.</param>
        public FeatureCollection(List<Feature> features)
        {
            this.Features = features;

            this.Type = GeoJSONObjectType.FeatureCollection;
        }

        /// <summary>
        /// Gets the features.
        /// </summary>
        /// <value>The features.</value>
        [JsonProperty(PropertyName = "features", Required = Required.Always)]
        public List<Feature> Features { get; private set; }
    }
}
