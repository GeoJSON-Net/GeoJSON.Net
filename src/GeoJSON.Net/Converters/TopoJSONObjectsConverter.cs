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
    using TopoJSON.Net;

    /// <summary>
    /// Defines the GeometryObject type. Converts to/from a SimpleGeo 'geometry' field
    /// </summary>
    public class TopoJSONObjectsConverter : JsonConverter
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
            JObject objects = serializer.Deserialize<JObject>(reader);
            List<TopoJSONNamedObjectWrapper> geoJSONObjects = new List<TopoJSONNamedObjectWrapper>();
            foreach (var geometryObject in objects) {
                String id = geometryObject.Key;
                JObject geometry = geometryObject.Value as JObject;
                var converter = new TopoJSONGeometryConverter();
                IGeometryObject igo  = (IGeometryObject)converter.ReadJson(reader, typeof(IGeometryObject), geometry, serializer);
                TopoJSONNamedObjectWrapper wrapper = new TopoJSONNamedObjectWrapper(id, igo);
                // This is so dirty that it hurts makes my eyes melt into my brain. 
                //(igo as TopoJSONObject).name = id;
                geoJSONObjects.Add(wrapper);
            }
            return geoJSONObjects;
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
