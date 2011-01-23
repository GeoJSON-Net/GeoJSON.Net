// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeometryCollection.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the <see cref="http://geojson.org/geojson-spec.html#geometry-collection">GeometryCollection</see> type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Geometry
{
    using System.Collections.Generic;

    using GeoJSON.Net.Converters;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines the <see cref="http://geojson.org/geojson-spec.html#geometry-collection">GeometryCollection</see> type.
    /// </summary>
    public class GeometryCollection : GeoJSONObject, IGeometryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeometryCollection"/> class.
        /// </summary>
        /// <param name="geometries">The geometries contained in this GeometryCollection.</param>
        public GeometryCollection(List<IGeometryObject> geometries = null)
        {
            this.Geometries = geometries ?? new List<IGeometryObject>();
            this.Type = GeoJSONObjectType.GeometryCollection;
        }

        /// <summary>
        /// Gets the list of Polygons enclosed in this MultiPolygon.
        /// </summary>
        [JsonProperty(PropertyName = "geometries", Required = Required.Always)]
        [JsonConverter(typeof(PositionConverter))]
        public List<IGeometryObject> Geometries { get; private set; }
    }
}
