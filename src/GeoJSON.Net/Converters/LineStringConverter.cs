// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PolygonConverter.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the LineString type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    ///     Converter to read and write the <see cref="LineString" /> type.
    /// </summary>
    public class LineStringConverter : JsonConverter
    {
        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LineString);
        }

        /// <summary>
        ///     Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        ///     The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            double[][] coordinates = existingValue == null
                ? serializer.Deserialize<double[][]>(reader)
                : (double[][])existingValue;

            IList<IPosition> positions = new List<IPosition>(coordinates.Length);

            foreach (var coordinate in coordinates)
            {
                var longitude = coordinate[0];
                var latitude = coordinate[1];
                double? altitude = null;
                double?[] m = null;

                if (coordinate.Length > 2)
                {
                    altitude = coordinate[2];
                }
                if (coordinate.Length > 3)
                {
                    m = new double?[coordinate.Length - 3];
                    int mIndex = 0;
                    for (int i = 3; i < coordinate.Length; i++)
                    {
                        m[mIndex] = coordinate[i];
                        mIndex++;
                    }
                }

                positions.Add(new GeographicPosition(latitude, longitude, altitude, m));
            }

            return positions;
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var coordinateElements = value as List<IPosition>;
            if (coordinateElements != null && coordinateElements.Count > 0)
            {
                var coordinateArray = new JArray();

                foreach (var position in coordinateElements)
                {
                    // TODO: position types should expose a double[] coordinates property that can be used to write values 
                    var coordinates = (GeographicPosition)position;
                    var coordinateElement = new JArray(coordinates.Longitude, coordinates.Latitude);
                    if (coordinates.Altitude.HasValue)
                    {
                        coordinateElement = new JArray(coordinates.Longitude, coordinates.Latitude, coordinates.Altitude);
                    }
                    if (coordinates.M != null && coordinates.M.Length > 0)
                    {
                        coordinateElement = new JArray(coordinates.Longitude, coordinates.Latitude, coordinates.Altitude, coordinates.M);
                    }

                    coordinateArray.Add(coordinateElement);
                }

                serializer.Serialize(writer, coordinateArray);
            }
            else
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}