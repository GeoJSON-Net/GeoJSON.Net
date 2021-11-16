// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    /// Converts <see cref="IGeometryObject"/> types to and from JSON.
    /// </summary>
    public class FeatureConverter : JsonConverter<object>
    {
        private static readonly GeometryConverter geometryConverter = new GeometryConverter();
        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(Feature.Feature).IsAssignableFromType(objectType) 
                || typeof(Feature.Feature<>).IsAssignableFromType(objectType) 
                || typeof(Feature.Feature<,>).IsAssignableFromType(objectType);
        }

        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var genericArguments = new Type[2];
            IGeometryObject geometryObject = null;
            object properties = null;
            string id = null;
            if (reader.TokenType == JsonTokenType.StartObject)
            {
                var startDepth = reader.CurrentDepth;
                while (reader.Read())
                {
                    if (JsonTokenType.EndObject == reader.TokenType && reader.CurrentDepth == startDepth)
                    {
                        if (genericArguments.Length == 0)
                        {
                            return new Feature.Feature(geometryObject, (IDictionary<string, object>)properties, id);
                        }
                        else if (genericArguments.Length == 1)
                        {
                            var typedGeometry = Convert.ChangeType(geometryObject, genericArguments[0]);
                            return Activator.CreateInstance(
                                typeof(Feature<>).MakeGenericType(
                                    genericArguments),
                                BindingFlags.Default,
                                binder: null,
                                args: new object[] { typedGeometry, (IDictionary<string, object>)properties, id },
                                culture: null);
                        }
                        else
                        {
                            var typedGeometry = Convert.ChangeType(geometryObject, genericArguments[0]);
                            var typedProperty = Convert.ChangeType(properties, genericArguments[1]);
                            return Activator.CreateInstance(
                                typeof(Feature<,>).MakeGenericType(
                                    genericArguments),
                                BindingFlags.Default,
                                binder: null,
                                args: new object[] { typedGeometry, typedProperty, id },
                                culture: null);
                        }
                    }

                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        var propertyName = reader.GetString();
                        genericArguments = typeToConvert.GetGenericArguments();
                        if (propertyName == "geometry")
                        {
                            reader.Read(); //Move one step forward to get property value
                            geometryObject = geometryConverter.Read(ref reader, typeof(IGeometryObject), options);
                        }
                        else if (propertyName == "properties")
                        {
                            if (genericArguments.Length > 1)
                            {
                                var typeOfProperties = typeToConvert.GetGenericArguments()[1]; // This is the argument for the property if set
                                properties = JsonSerializer.Deserialize(ref reader, typeOfProperties, options);
                            }
                            else
                            {
                                properties = JsonSerializer.Deserialize<IDictionary<string, object>>(ref reader, options);
                            }
                        }
                        else if (propertyName == "id")
                        {
                            reader.Read(); //Move one step forward to get property value
                            id = reader.GetString();
                        }
                    }
                }

                throw new JsonException("End of object was not found");
            }
            else
            {
                throw new JsonException("Json to parse of not of type object");
            }
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}