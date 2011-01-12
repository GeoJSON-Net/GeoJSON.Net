// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimestampConverter.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the PolygonConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SimpleGeo.Net.Helpers.Json.Converters
{
    using System;

    using GeoJSON.Net;
    using GeoJSON.Net.Exceptions;

    using Newtonsoft.Json;

    /// <summary>
    /// Converter to read and write the <see cref="MultiPolygon" /> type.
    /// </summary>
    public class TimestampConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param><param name="value">The value.</param><param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
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
            var dateTime = serializer.Deserialize<int?>(reader);
            if (dateTime == null)
            {
                throw new ParsingException("DateTime property could not be parsed. Expected something like '1278696668'");
            }

            try
            {
                return Helpers.DateTime.ConvertFromUnixTimestamp((double)dateTime);
            }
            catch (Exception ex)
            {
                throw new ParsingException(innerException: ex);
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
            return objectType == typeof(DateTime);
        }
    }
}
