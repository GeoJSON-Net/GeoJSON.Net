// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeometryConverter.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the GeometryConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GeoJSON.Net.Exceptions;
    using GeoJSON.Net.Geometry;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Serialization;

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
            // ToDo: implement
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
            var geometry = serializer.Deserialize<Dictionary<string, object>>(reader);
            JArray coordinates;
            try
            {
                coordinates = (JArray)geometry["coordinates"];
                if (coordinates == null || !coordinates.HasValues)
                {
                    throw new ParsingException("Could not Parse SimpleGeo Response. (Coordinates missing?)");
                }
            }
            catch (Exception ex)
            {
                throw new ParsingException("Could not Parse SimpleGeo Response. (Coordinates missing?)", ex);
            }

            var parsingErrors = new List<Exception>();

            // ToDo: tidy up redundancies
            var geometryType = (string)geometry["type"];
            switch (geometryType.Trim())
            {
                case null:
                case "":
                    return null;
                case "Point":
                    var pointDeserializerSettings = new JsonSerializerSettings
                            {
                                Error = delegate(object sender, ErrorEventArgs args)
                                    {
                                        parsingErrors.Add(args.ErrorContext.Error);
                                        args.ErrorContext.Handled = true;
                                    },
                                Converters = { new PositionConverter() }
                            };
                    var point = JsonConvert.DeserializeObject<Point>(
                        coordinates.ToString(),
                        pointDeserializerSettings);

                    if (parsingErrors.Any())
                    {
                        throw new AggregateException("Error Parsing GeometryObject.", parsingErrors);
                    }

                    return point;
                case "Polygon":
                    var polygonDeserializerSettings = new JsonSerializerSettings
                            {
                                Error = delegate(object sender, ErrorEventArgs args)
                                    {
                                        parsingErrors.Add(args.ErrorContext.Error);
                                        args.ErrorContext.Handled = true;
                                    },
                                Converters = { new PolygonConverter() }
                            };
                    var polygon = JsonConvert.DeserializeObject<Polygon>(
                        coordinates.ToString(),
                        polygonDeserializerSettings);

                    if (parsingErrors.Any())
                    {
                        throw new AggregateException("Error parsing GeometryObject.", parsingErrors);
                    }

                    return polygon;

                case "MultiPolygon":
                    var multipolygonDeserializerSettings = new JsonSerializerSettings
                            {
                                Error = delegate(object sender, ErrorEventArgs args)
                                    {
                                        parsingErrors.Add(args.ErrorContext.Error);
                                        args.ErrorContext.Handled = true;
                                    },
                                Converters = { new MultiPolygonConverter() }
                            };
                    var multiPolygon = JsonConvert.DeserializeObject<MultiPolygon>(
                        coordinates.ToString(),
                        multipolygonDeserializerSettings);

                    if (parsingErrors.Any())
                    {
                        throw new AggregateException("Error parsing GeometryObject.", parsingErrors);
                    }

                    return multiPolygon;
                default:
                    throw new ParsingException(string.Format("Unknown geometry type '{0}' cannot be parsed.", geometryType));
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
            if (objectType.IsInterface)
            {
                return objectType == typeof(IGeometryObject);
            }

            return objectType.GetInterface(typeof(IGeometryObject).Name, true) != null;
        }
    }
}
