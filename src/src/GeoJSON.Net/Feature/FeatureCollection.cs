// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeatureCollection.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the FeatureCollection type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GeoJSON.Net.Feature
{
    /// <summary>
    ///     Defines the FeatureCollection type.
    /// </summary>
    public class FeatureCollection : GeoJSONObject
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FeatureCollection" /> class.
        /// </summary>
        public FeatureCollection() : this(new List<Feature>())
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FeatureCollection" /> class.
        /// </summary>
        /// <param name="features">The features.</param>
        public FeatureCollection(List<Feature> features)
        {
            if (features == null)
            {
                throw new ArgumentNullException("features");
            }

            Features = features;
            Type = GeoJSONObjectType.FeatureCollection;
        }

        /// <summary>
        ///     Gets the features.
        /// </summary>
        /// <value>The features.</value>
        [JsonProperty(PropertyName = "features", Required = Required.Always)]
        public List<Feature> Features { get; private set; }
    }
}