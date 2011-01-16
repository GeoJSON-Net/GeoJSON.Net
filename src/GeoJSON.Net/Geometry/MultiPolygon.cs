// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiPolygon.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the MultiPolygon type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Geometry
{
    using System.Collections.Generic;

    using GeoJSON.Net.Converters;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines the MultiPolygon type.
    /// </summary>
    public class MultiPolygon : GeoJSONObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPolygon"/> class.
        /// </summary>
        /// <param name="polygons">The polygons contained in this MultiPolygon.</param>
        public MultiPolygon(List<Polygon> polygons = null)
        {
            this.Coordinates = polygons ?? new List<Polygon>();
            this.Type = GeoJSONObjectType.MultiPolygon;
        }

        /// <summary>
        /// Gets the list of Polygons enclosed in this MultiPolygon.
        /// </summary>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(PositionConverter))]
        public List<Polygon> Coordinates { get; private set; }
    }
}
