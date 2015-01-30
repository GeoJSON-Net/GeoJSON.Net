using System;
using GeoJSON.Net.CoordinateReferenceSystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeoJSON.Net.Converters
{
    class CrsConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var typeName = jsonObject["type"].ToString().ToLower();

            switch (typeName)
            {
                case "name":
                    JObject propName = null;
                    JToken jt;
                    if (jsonObject.TryGetValue("properties", out jt))
                    {
                        propName = JObject.Parse(jt.ToString());
                    }

                    if (propName != null)
                    {
                        var target = new NamedCRS(propName["name"].ToString());
                        serializer.Populate(jsonObject.CreateReader(), target);
                        return target;
                    }
                    break;
                case "link":
                    var linked = new LinkedCRS("");
                    serializer.Populate(jsonObject.CreateReader(), linked);
                    return linked;
            }

            return new NotSupportedException(string.Format("Type {0} unexpected.", typeName));
        }


        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsInterface)
            {
                return objectType == typeof(ICRSObject);
            }

            return typeof(ICRSObject).IsAssignableFrom(objectType);
        }


    }
}
