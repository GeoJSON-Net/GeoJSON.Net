// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PositionConverter.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the PolygonConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using GeoJSON.Net.Exceptions;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    ///     Converter to read and write the <see cref="Point" /> type.
    /// </summary>
    public class PointConverter : JsonConverter
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
            return typeof(GeographicPosition).IsAssignableFrom(objectType);
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
            double[] coordinates = null;

            try
            {
                coordinates = serializer.Deserialize<double[]>(reader);
            }
            catch (Exception e)
            {
                throw new JsonReaderException("error parsing coordinates", e);
            }

            if (coordinates == null)
            {
                throw new JsonReaderException("coordinates cannot be null");
            }

            if (coordinates.Length != 2 && coordinates.Length != 3)
            {
                throw new JsonReaderException(
                    string.Format("Expected 2 or 3 coordinates but received {0}", coordinates));
            }

            var longitude = coordinates[0];
            var latitude = coordinates[1];
            double? altitude = null;

            if (coordinates.Length == 3)
            {
                altitude = coordinates[2];
            }

            return new GeographicPosition(latitude, longitude, altitude);
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var coordinates = value as GeographicPosition;
            if (coordinates != null)
            {
                writer.WriteStartArray();

                writer.WriteValue(coordinates.Longitude);
                writer.WriteValue(coordinates.Latitude);

                if (coordinates.Altitude.HasValue)
                {
                    writer.WriteValue(coordinates.Altitude.Value);
                }

                writer.WriteEndArray();
            }
            else
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}