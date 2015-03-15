using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopoJSON.Net.Converters
{
    using GeoJSON.Net.Geometry;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using TopoJSON.Net.Geometry;

    /// <summary>
    /// Transformation info serializer.
    /// </summary>
    public class TopoJSONTransformationConverter : JsonConverter
    {
        /// <summary>
        /// Converts if this is a transformation info instance.
        /// </summary>
        /// <param name="objectType">The object type</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TopoJSONTransformationInfo);
        }

        /// <summary>
        /// Not implemented yet.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject transform = serializer.Deserialize<JObject>(reader);
            var jScale = transform["scale"] as JArray;
            double[] scale = jScale.Select(s => (double)s).ToArray();
            var jTranslate = transform["translate"] as JArray;
            double[] translate = jTranslate.Select(t => (double)t).ToArray();

            return new TopoJSONTransformationInfo() {
                isQuantized = true,
                Scale = scale,
                Translation = translate
            };
        }

        /// <summary>
        /// Serializes the transformation info.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
