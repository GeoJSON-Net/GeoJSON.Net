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
    public class FeatureCollection : Feature
    {
        /// <summary>
        /// Gets the features.
        /// </summary>
        /// <value>The features.</value>
        [JsonProperty(PropertyName = "feature", Required = Required.Always)]
        public List<Feature> Features { get; private set; }
    }
}
