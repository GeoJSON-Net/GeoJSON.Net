// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeoJSONObject.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the GeoJSONObject type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json.Converters;

namespace GeoJSON.Net
{
    using Newtonsoft.Json;

    /// <summary>
    /// Base class for all IGeometryObject implementing types
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class GeoJSONObject : IGeoJSONObject
    {
        /// <summary>
        /// Gets the (mandatory) type of the <see cref="http://geojson.org/geojson-spec.html#geojson-objects">GeoJSON Object</see>.
        /// </summary>
        /// <value>
        /// The type of the object.
        /// </value>
        [JsonProperty(PropertyName = "type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public GeoJSONObjectType Type { get; internal set; }

        /// <summary>
        /// Gets or sets the (optional) <see cref="http://geojson.org/geojson-spec.html#coordinate-reference-system-objects">Coordinate Reference System Object</see>.
        /// </summary>
        /// <value>
        /// The Coordinate Reference System Objects.
        /// </value>
        [JsonProperty(PropertyName = "crs", Required = Required.Default)]
        public CoordinateReferenceSystem.ICRSObject CRS { get; set; }

        /// <summary>
        /// Gets or sets the (optional) <see cref="http://geojson.org/geojson-spec.html#coordinate-reference-system-objects">Bounding Boxes</see>.
        /// </summary>
        /// <value>
        /// The value of the bbox member must be a 2*n array where n is the number of dimensions represented in the
        /// contained geometries, with the lowest values for all axes followed by the highest values.
        /// The axes order of a bbox follows the axes order of geometries.
        /// In addition, the coordinate reference system for the bbox is assumed to match the coordinate reference
        /// system of the GeoJSON object of which it is a member.
        /// </value>
        [JsonProperty(PropertyName = "bbox", Required = Required.Default)]
        public double[] BoundingBoxes { get; set; }
    }
}
