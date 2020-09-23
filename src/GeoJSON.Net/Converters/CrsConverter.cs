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
            if (reader.TokenType == JsonTokenType.Null) {
                return new UnspecifiedCRS();
            }
            if (reader.TokenType != JsonTokenType.PropertyName && reader.GetString() != "crs") {
                throw new JsonException("CRS must be null or a json object");
            }

            ICRSObject crs = null;

            while (reader.Read()) {
                if (reader.TokenType == JsonTokenType.EndObject) {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName) {
                    continue;
                }

                var propertyName = reader.GetString();

                if (propertyName != "type") {
                    throw new JsonException();
                }

                // advance to property
                reader.Read();
                var crsType = reader.GetString();

                reader.Read();

                if (reader.TokenType != JsonTokenType.PropertyName && reader.GetString() != "properties") {
                    throw new JsonException();
                }

                switch(crsType) {
                    case "name":
                        var name = string.Empty;

                        while (reader.Read()) {
                            if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "name")
                            {
                                reader.Read();
                                name = reader.GetString();
                            }

                            if (reader.TokenType == JsonTokenType.EndObject) {
                                crs = new NamedCRS(name);
                                break;
                            }
                        }
                        break;
                    case "link":
                        var href = string.Empty;
                        var linkType = string.Empty;

                        while (reader.Read()) {
                            if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "href") {
                                reader.Read();
                                href = reader.GetString();

                                continue;
                            }

                            if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "type") {
                                reader.Read();
                                linkType = reader.GetString();

                                continue;
                            }

                            if (reader.TokenType == JsonTokenType.EndObject) {
                                if (string.IsNullOrEmpty(linkType)) {
                                    crs = new LinkedCRS(href);
                                } else {
                                    crs = new LinkedCRS(href, linkType);
                                }

                                break;
                            }
                        }
                        break;
                }
            }

            return crs;
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
            switch (value)
            {
                case NamedCRS named:
                    writer.WriteStartObject("crs");
                    writer.WriteString("type", "name");
                    writer.WritePropertyName("properties");
                    JsonSerializer.Serialize(writer, named.Properties, options);
                    writer.WriteEndObject();
                    break;
                case LinkedCRS linked:
                    writer.WriteStartObject("crs");
                    writer.WriteString("type", "link");
                    writer.WritePropertyName("properties");
                    JsonSerializer.Serialize(writer, linked.Properties, options);
                    writer.WriteEndObject();
                    break;
                case UnspecifiedCRS:
                    writer.WriteNull("crs");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
