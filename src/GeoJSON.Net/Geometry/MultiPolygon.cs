// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiPolygon.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the <see href="http://geojson.org/geojson-spec.html#multipolygon">MultiPolygon</see> type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Geometry
{
    using System.Collections.Generic;

    using GeoJSON.Net.Converters;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines the <see href="http://geojson.org/geojson-spec.html#multipolygon">MultiPolygon</see> type.
    /// </summary>
    public class MultiPolygon : GeoJSONObject, IGeometryObject
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
        [JsonConverter(typeof(MultiPolygonConverter))]
        public List<Polygon> Coordinates { get; private set; }
    }
}
