using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopoJSON.Net.Geometry
{
    using GeoJSON.Net;
    using GeoJSON.Net.Converters;
    using GeoJSON.Net.Geometry;
    using Newtonsoft.Json;
    using System.Runtime.Serialization;
    using TopoJSON.Net.Converters;

    /// <summary>
    /// The topology type is the root for all TopoJSON objects and 
    /// encapsulates objects and arcs.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Topology : TopoJSONObject, IGeometryObject
    {
        #region ---------- Objects ----------
        /// <summary>
        /// The list of arcs.
        /// </summary>
        [JsonProperty(PropertyName = "objects", Required = Required.Default, Order = 50)]
        [JsonConverter(typeof(TopoJSONObjectsConverter))]
        public List<TopoJSONNamedObjectWrapper> Objects { get; set; }
        #endregion

        #region ---------- Arcs ----------
        private List<Arc> _arcs;

        /// <summary>
        /// The list of arcs.
        /// </summary>
        [JsonProperty(PropertyName = "arcs", Required = Required.Always, Order = 100)]
        [JsonConverter(typeof(ArcsConverter))]
        public List<Arc> Arcs
        {
            get { return _arcs; }
            set { _arcs = value; }
        }
        #endregion

        #region ---------- Transformation ----------
        /// <summary>
        /// The transformation information
        /// </summary>
        [JsonProperty(PropertyName = "transform", Required = Required.Default, Order = 10)]
        [JsonConverter(typeof(TopoJSONTransformationConverter))]
        public TopoJSONTransformationInfo Transform { get; set; }
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Topology() {
            this.Type = GeoJSONObjectType.Topology;
        }

        /// <summary>
        /// This method is called after the deserialization. It basically restores 
        /// the object consistency by iterating over all geometries and assigning 
        /// the correct coordinates (using the arcs index).
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            // Iterate over all objects
            foreach (var wrapper in Objects) {
                IGeometryObject igo = wrapper.Geometry;
                this.restoreGeometryCoordinates(igo);
            }
        }

        /// <summary>
        /// Restores the coordinates for a given Geometry Object.
        /// </summary>
        /// <param name="igo">The Geometry Object</param>
        private void restoreGeometryCoordinates(IGeometryObject igo) {
            // Go over all types. Point and MultiPoint can be left out here. 
            switch (igo.Type)
            {
                // LineStrings
                case GeoJSONObjectType.LineString:
                    TopoJSONLineString ls = igo as TopoJSONLineString;
                    ls.Coordinates = this.getCoordinatesFromArcsIndex(ls.ArcIdx);
                    break;
                // MultiLineStrings
                case GeoJSONObjectType.MultiLineString:
                    TopoJSONMultiLineString mls = igo as TopoJSONMultiLineString;
                    foreach (var idxArray in mls.ArcIdx) {
                        List<GeographicPosition> lsCoords = this.getCoordinatesFromArcsIndex(idxArray);
                        TopoJSONLineString l = new TopoJSONLineString(lsCoords);
                        mls.Coordinates.Add(l);
                    }
                    break;
                // GeometryCollections
                case GeoJSONObjectType.GeometryCollection:
                    GeometryCollection gc = igo as GeometryCollection;
                    foreach (var geometry in gc.Geometries) {
                        this.restoreGeometryCoordinates(geometry);
                    }
                    break;
                // Polygon
                case GeoJSONObjectType.Polygon:
                    TopoJSONPolygon poly = igo as TopoJSONPolygon;
                    List<GeographicPosition> coordinates = new List<GeographicPosition>();
                    foreach (var idxArray in poly.ArcIdx)
                    {
                        coordinates = this.getCoordinatesFromArcsIndex(idxArray);
                    }
                    poly.Coordinates = coordinates;
                    break;
                // MultiPolygon
                case GeoJSONObjectType.MultiPolygon:
                    TopoJSONMultiPolygon mpoly = igo as TopoJSONMultiPolygon;
                    foreach (var idxArray in mpoly.ArcIdx)
                    {
                        foreach (var idx in idxArray)
                        {
                            List<GeographicPosition> polyCoords = new List<GeographicPosition>();
                            polyCoords = this.getCoordinatesFromArcsIndex(idx);
                            TopoJSONPolygon tp = new TopoJSONPolygon(polyCoords);
                            mpoly.Coordinates.Add(tp);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// From a given arcs index this method retrieves and decodes all coordinates
        /// and stores them in a list.
        /// </summary>
        /// <param name="idxArray">The index.</param>
        /// <returns>A list of coordinates.</returns>
        private List<GeographicPosition> getCoordinatesFromArcsIndex(List<int> idxArray) {
            List<GeographicPosition> coordinates = new List<GeographicPosition>();
            foreach (var idx in idxArray)
            {
                Arc arc = new Arc();
                // We have to invert negative indexes
                if (idx < 0)
                {
                    int realIndex = Math.Abs(idx) - 1;
                    // We're getting acopy here since we plan to modify it.
                    arc.Positions = this.Arcs[realIndex].Positions.ToList();
                    arc.Positions.Reverse();
                }
                else
                {
                    arc.Positions = this.Arcs[idx].Positions.ToList();
                }
                arc = this.decodeArc(arc);
                coordinates.AddRange(arc.Positions);
            }
            return coordinates;
        }

        /// <summary>
        /// Decodes the positions of an arc. This is pretty much a 1:1 conversion of the method
        /// suggested here: https://github.com/topojson/topojson-specification/blob/master/README.md#213-arcs
        /// </summary>
        /// <param name="arc">The arc.</param>
        /// <returns>The decoded arc.</returns>
        private Arc decodeArc (Arc arc) {
            if (arc == null)
                return arc;
            // Decoding is only needed if the arc is quantized.
            if (this.Transform == null)
                return arc;
            Arc decodedArc = new Arc();
            List<GeographicPosition> positions = new List<GeographicPosition>();
            double x = 0, y = 0;
            foreach (var position in arc.Positions)
            {
                // TODO: Switch latitude and longitude
                double lat = (x += position.Longitude) * this.Transform.Scale[0] +  this.Transform.Translation[0];
                double lon = (y += position.Latitude) * this.Transform.Scale[1] +  this.Transform.Translation[1];
                positions.Add(new GeographicPosition(lat, lon));
            }
            decodedArc.Positions = positions;
            return decodedArc;
        }
    }
}
