// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeometryConverter.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the GeometryConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Converters
{
    using System;

    using GeoJSON.Net.Geometry;
    using System.Collections;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Serialization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines the GeometryObject type. Converts to/from a SimpleGeo 'geometry' field
    /// </summary>
    public class GeometryConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param><param name="value">The value.</param><param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Point
                || value is Polygon
                || value is LineString
                || value is MultiLineString
                || value is MultiPolygon)
            {
                serializer.Serialize(writer, value);
            }
            else
            {
                // TODO: implement
                throw new NotImplementedException(string.Format("Serialization of {0} not implemented.",
                    value.GetType().Name));
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
            var inputJsonValue = serializer.Deserialize(reader).ToString();
            inputJsonValue = inputJsonValue.Replace(Environment.NewLine, "");
            inputJsonValue = inputJsonValue.Replace(" ", "");

            var geoType = Regex.Match(inputJsonValue, @"type\W+(?<type>\w+)\W+");
            var geoTypeString = geoType.Groups["type"].Value.ToLowerInvariant();

            switch (geoTypeString) 
            { 
                case "polygon":
                    return JsonConvert.DeserializeObject<Polygon>(inputJsonValue, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    break;
                case "point":
                    return JsonConvert.DeserializeObject<Point>(inputJsonValue, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    break;
            }

            // ToDo: implement
            throw new NotImplementedException();
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
            if (objectType.IsInterface)
            {
                return objectType == typeof(IGeometryObject);
            }

            return typeof(IGeometryObject).IsAssignableFrom(objectType);
        }
    }
}
