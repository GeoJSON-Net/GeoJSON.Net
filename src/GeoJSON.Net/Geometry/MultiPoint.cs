// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiPoint.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the MultiPoint type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Geometry
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains an array of <see cref="Point"/>s.
    /// </summary>
    /// <seealso cref="http://geojson.org/geojson-spec.html#multipoint"/>
    public class MultiPoint : TopoJSONObject, IGeometryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPoint"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public MultiPoint(List<Point> coordinates = null)
        {
            this.Coordinates = coordinates ?? new List<Point>();
            this.Type = GeoJSONObjectType.MultiPoint;
        }
        
        /// <summary>
        /// Gets the Coordinates.
        /// </summary>
        /// <value>The Coordinates.</value>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        public List<Point> Coordinates { get; private set; }
    }
}
