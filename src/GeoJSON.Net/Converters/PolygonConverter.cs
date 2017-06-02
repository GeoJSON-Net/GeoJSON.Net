// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System;
using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    /// Converter to read and write the <see cref="Polygon" /> type.
    /// </summary>
    public class PolygonConverter : JsonConverter
    {
        private static readonly LineStringConverter LineStringConverter = new LineStringConverter();

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

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var rings = serializer.Deserialize<double[][][]>(reader);
            var lineStrings = new List<LineString>(rings.Length);

            foreach (var ring in rings)
            {
                var positions = (IEnumerable<IPosition>)LineStringConverter.ReadJson(reader, typeof(LineString), ring, serializer);
                lineStrings.Add(new LineString(positions));
            }

            return lineStrings;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var coordinateElements = value as List<LineString>;
            if (coordinateElements != null && coordinateElements.Count > 0)
            {
                if (coordinateElements[0].Coordinates[0] is Position)
                {
                    writer.WriteStartArray();

                    foreach (var subPolygon in coordinateElements)
                    {
                        LineStringConverter.WriteJson(writer, subPolygon.Coordinates, serializer);
                    }

                    writer.WriteEndArray();
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}