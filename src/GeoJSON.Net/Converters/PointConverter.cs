// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PositionConverter.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the PolygonConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Converters
{
    using System;

    using Exceptions;
    using Geometry;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Converter to read and write the <see cref="GeographicPosition" /> type.
    /// </summary>
    public class PointConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param><param name="value">The value.</param><param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var coordinates = value as GeographicPosition;
            if (coordinates != null)
            {
                var coordinateArray = coordinates.Altitude.HasValue && coordinates.Altitude != 0
                    ? new JArray(coordinates.Longitude, coordinates.Latitude, coordinates.Altitude)
                    : new JArray(coordinates.Longitude, coordinates.Latitude);

                serializer.Serialize(writer, coordinateArray);
            }
            else
            {
                serializer.Serialize(writer, value);
            }
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
            var coordinates = serializer.Deserialize<JArray>(reader);

            if (coordinates == null || coordinates.Count != 2)
            {
                throw new ParsingException(
                    string.Format(
                        "Point geometry coordinates could not be parsed. Expected something like '[-122.428938,37.766713]' ([lon,lat]), what we received however was: {0}", 
                        coordinates));
            }

            try
            {
                var longitude = coordinates.First.Value<double>();
                var latitude = coordinates.Last.Value<double>();
                return new GeographicPosition(latitude, longitude);
            }
            catch (Exception ex)
            {
                throw new ParsingException("Could not parse GeoJSON Response. (Latitude or Longitude missing from Point geometry?)", ex);
            }
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
            return typeof(GeographicPosition).IsAssignableFrom(objectType);
        }
    }
}
