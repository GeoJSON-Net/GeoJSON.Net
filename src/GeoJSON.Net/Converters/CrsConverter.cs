// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System;
using GeoJSON.Net.CoordinateReferenceSystem;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    /// Converts <see cref="ICRSObject"/> types to and from JSON.
    /// </summary>
    public class CrsConverter : JsonConverter<ICRSObject>
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(ICRSObject).IsAssignableFromType(objectType);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        /// <exception cref="Newtonsoft.Json.JsonReaderException">
        /// CRS must be null or a json object
        ///     or
        /// CRS must have a "type" property
        /// </exception>
        public override ICRSObject Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            // if (reader.TokenType == JsonToken.Null)
            // {
            //     return new UnspecifiedCRS();
            // }
            // if (reader.TokenType != JsonToken.StartObject)
            // {
            //     throw new JsonReaderException("CRS must be null or a json object");
            // }

            // var jObject = JObject.Load(reader);

            // JToken token;
            // if (!jObject.TryGetValue("type", StringComparison.OrdinalIgnoreCase, out token))
            // {
            //     throw new JsonReaderException("CRS must have a \"type\" property");
            // }

            // var crsType = token.Value<string>();

            // if (string.Equals("name", crsType, StringComparison.OrdinalIgnoreCase))
            // {
            //     JObject properties = null;
            //     if (jObject.TryGetValue("properties", out token))
            //     {
            //         properties = token as JObject;
            //     }

            //     if (properties != null)
            //     {
            //         var target = new NamedCRS(properties["name"].ToString());
            //         serializer.Populate(jObject.CreateReader(), target);
            //         return target;
            //     }
            // }
            // else if (string.Equals("link", crsType, StringComparison.OrdinalIgnoreCase))
            // {
            //     JObject properties = null;
            //     if (jObject.TryGetValue("properties", out token))
            //     {
            //         properties = token as JObject;
            //     }

            //     if (properties != null)
            //     {
            //         var linked = new LinkedCRS(properties["href"].ToString());
            //         serializer.Populate(jObject.CreateReader(), linked);
            //         return linked;
            //     }
            // }

            // return new NotSupportedException(string.Format("Type {0} unexpected.", crsType));

            return null;
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public override void Write(Utf8JsonWriter writer, ICRSObject value, JsonSerializerOptions options)
        {
            switch (value.Type)
            {
                case CRSType.Name:
                    JsonSerializer.Serialize(writer, value, options);
                    break;
                case CRSType.Link:
                    JsonSerializer.Serialize(writer, value, options);
                    break;
                case CRSType.Unspecified:
                    writer.WriteNull("crs");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
