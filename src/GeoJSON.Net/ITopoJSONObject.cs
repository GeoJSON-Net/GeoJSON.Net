// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITopoJSONObject.cs" company="Friedrich Politz (Original work by Joerg Battermann)">
//   Copyright © Joerg Battermann 2014
//   Copyright © Friedrich Politz 2015
// </copyright>
// <summary>
//   Defines the ITopoJSONObject interface based on the work of Joerg Battermann keeping the 
//   modifications as little as possible to ensure compatibility.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using GeoJSON.Net;
namespace TopoJSON.Net
{
    /// <summary>
    /// Base Interface for TopoJSONObject types.
    /// </summary>
    public interface ITopoJSONObject
    {
        /// <summary>
        /// Gets the (mandatory) type of the <see href="https://github.com/topojson/topojson-specification/blob/master/README.md#22-geometry-objects">TopoJSON Object</see>.
        /// </summary>
        /// <value>
        /// The type of the object.
        /// </value>
        GeoJSONObjectType Type { get; }

        /// <summary>
        /// Gets or sets the (optional) <see href="https://github.com/topojson/topojson-specification/blob/master/README.md#3-bounding-boxes">Bounding Boxes</see>.
        /// </summary>
        /// <value>
        /// The value of the bbox member must be a 2*n array where n is the number of dimensions represented in the
        /// contained geometries, with the lowest values for all axes followed by the highest values.
        /// The axes order of a bbox follows the axes order of geometries. The bounding box should not be transformed using the topology’s transform, if any.
        /// </value>
        double[] BoundingBoxes { get; set; }
    }
}
