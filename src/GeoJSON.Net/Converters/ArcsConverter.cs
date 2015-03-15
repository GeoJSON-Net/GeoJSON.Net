using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopoJSON.Net.Converters
{
    using GeoJSON.Net.Converters;
    using GeoJSON.Net.Geometry;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Serializes a list of arcs into JSON.
    /// </summary>
    public class ArcsConverter : JsonConverter
    {
        /// <summary>
        /// Converts if we have a list of arcs.
        /// </summary>
        /// <param name="objectType">The object type</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IEnumerable<Arc>);
        }

        /// <summary>
        /// Not implemented yet.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jArcs = serializer.Deserialize<JArray>(reader);
            List<Arc> arcs = new List<Arc>();
            if (jArcs == null || jArcs.Count == 0)
                return arcs;
            foreach (var arc in jArcs.Children()) {
                Arc a = new Arc();
                List<GeographicPosition> positions = new List<GeographicPosition>();
                var converter = new PointConverter();
                foreach (JArray coordinate in arc) {
                    GeographicPosition position;
                    if (coordinate.Count == 2)
                    {
                        position = new GeographicPosition((double)coordinate[1], (double)coordinate[0]);
                    }
                    else if (coordinate.Count == 3)
                    {
                        return (IPosition)new GeographicPosition((double)coordinate[1], (double)coordinate[0], (double)coordinate[2]);
                    }
                    else
                    {
                        throw new JsonException(string.Format("Coordinate must have two or three components ({0} found).",
                            coordinate.Count));
                    }
                    positions.Add(position);
                }
                a.Positions = positions;
                arcs.Add(a);
            }
            return arcs;
        }

        /// <summary>
        /// Serializes the arcs.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            IEnumerable<Arc> arcs = value as IEnumerable<Arc>;
            if (arcs == null)
            {
                serializer.Serialize(writer, value);
                return;
            }
            if (arcs == null)
                return;
            var arcsArray = new JArray();
            foreach (Arc a in arcs) {
                var coordinateArray = new JArray();
                foreach (GeographicPosition gp in a.Positions) {
                    var coordinate = new JArray(gp.Longitude, gp.Latitude);
                    if (gp.Altitude.HasValue)
                        coordinate = new JArray(gp.Longitude, gp.Latitude, gp.Altitude);
                    coordinateArray.Add(coordinate);
                }
                arcsArray.Add(coordinateArray);
            }
            serializer.Serialize(writer, arcsArray);
        }
    }
}
