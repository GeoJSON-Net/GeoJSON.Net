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
    using TopoJSON.Net.Geometry;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Defines the GeometryObject type. Converts to/from a SimpleGeo 'geometry' field
    /// </summary>
    public class TopoJSONGeometryConverter : JsonConverter
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
            JObject jObject = existingValue as JObject;
            String type = jObject["type"].Value<String>(); // TODO: Check for upper case and existence here!!!
            IGeometryObject igo = null;
            List<IGeometryObject> geoList = new List<IGeometryObject>();
            switch (type.ToLowerInvariant())
            {
                case "point":
                    igo = JsonConvert.DeserializeObject<Point>(jObject.ToString(), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    break;
                case "multipoint":
                    igo = JsonConvert.DeserializeObject<MultiPoint>(jObject.ToString(), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    break;
                case "polygon":
                    igo = JsonConvert.DeserializeObject<TopoJSONPolygon>(jObject.ToString(), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    break;
                case "multipolygon":
                    igo = JsonConvert.DeserializeObject<TopoJSONMultiPolygon>(jObject.ToString(), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    break;
                case "geometrycollection":
                    JToken geometries = JArray.FromObject(jObject["geometries"]); // TODO: Check if exists here.
                    List<IGeometryObject> subgeometries = new List<IGeometryObject>();
                    var converter = new TopoJSONGeometryConverter();
                    foreach (var geometryObject in geometries)
                    {
                        var ls  = (IGeometryObject)converter.ReadJson(reader, typeof(IGeometryObject), geometryObject, serializer);
                        subgeometries.Add(ls);
                    }
                    igo = new GeometryCollection(subgeometries);
                    break;
                case "linestring":
                    igo = JsonConvert.DeserializeObject<TopoJSONLineString>(jObject.ToString(), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    break;
            }
            return igo;
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
