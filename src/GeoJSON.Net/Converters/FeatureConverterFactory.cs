using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJSON.Net.Converters
{
    public class FeatureConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }

        public override JsonConverter CreateConverter(
            Type type,
            JsonSerializerOptions options)
        {
            //Type keyType = type.GetGenericArguments()[0];
            //Type valueType = type.GetGenericArguments()[1];

            return new FeatureConverter();
            //JsonConverter converter = (JsonConverter)Activator.CreateInstance(
            //    typeof(FeatureConverter).MakeGenericType(
            //        new Type[] { keyType, valueType }),
            //    BindingFlags.Instance | BindingFlags.Public,
            //    binder: null,
            //    args: new object[] { options },
            //    culture: null);

            //return converter;
        }
    }
}
