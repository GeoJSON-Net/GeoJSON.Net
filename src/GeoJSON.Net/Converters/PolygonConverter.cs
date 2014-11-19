// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PolygonConverter.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the PolygonConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Converters
{
    using System;

    using GeoJSON.Net.Geometry;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Globalization;

    /// <summary>
    /// Converter to read and write the <see cref="MultiPolygon" /> type.
    /// </summary>
    public class PolygonConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param><param name="value">The value.</param><param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var coordinateElements = value as System.Collections.Generic.List<GeoJSON.Net.Geometry.LineString>;
            if (coordinateElements != null && coordinateElements.Count > 0)
            {
                var coordinateArray = new JArray();
                if (coordinateElements[0].Coordinates[0] is GeographicPosition)
                {
                    foreach(var subPolygon in coordinateElements)
                    {
                        var subPolygonCoordinateArray = new JArray();
                        foreach (var coordinates in subPolygon.Coordinates.Select(x => x as GeographicPosition))
                        {
                            var coordinateElement = new JArray(coordinates.Longitude, coordinates.Latitude);
                            if (coordinates.Altitude.HasValue && coordinates.Altitude != 0)
                                coordinateElement = new JArray(coordinates.Longitude, coordinates.Latitude, coordinates.Altitude);

                            subPolygonCoordinateArray.Add(coordinateElement);
                        }
                        coordinateArray.Add(subPolygonCoordinateArray);
                    }
                    serializer.Serialize(writer, coordinateArray);
                }
                else
                    // ToDo: implement
                    throw new NotImplementedException();
            }
            else
                serializer.Serialize(writer, value);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param><param name="objectType">Type of the object.</param><param name="existingValue">The existing value of object being read.</param><param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var ringArray = serializer.Deserialize(reader) as JArray;
            var rings = ringArray.Select(ring => new LineString((ring as JArray).Select(coordinate =>
            {
                var cArray = coordinate as JArray;
                if (cArray.Count == 2)
                {
                    return (IPosition)new GeographicPosition((double) cArray[1], (double) cArray[0]);
                }
                else if (cArray.Count == 3)
                {
                    return (IPosition)new GeographicPosition((double)cArray[1], (double)cArray[0], (double)cArray[2]);
                }
                else
                {
                    throw new JsonException(string.Format("Coordinate must have two or three components ({0} found).",
                        cArray.Count));
                }
            }).ToList())).ToList();
            return rings;
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Polygon);
        }
    }
}
