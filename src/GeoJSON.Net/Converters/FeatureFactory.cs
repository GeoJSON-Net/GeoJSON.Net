// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System;
using GeoJSON.Net.Geometry;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using GeoJSON.Net.Feature;
using System.Reflection;
using GeoJSON.Net.CoordinateReferenceSystem;

namespace GeoJSON.Net.Converters {
    /// <summary>
    ///     Converter to read and write an <see cref="IPosition" />, that is,
    ///     the coordinates of a <see cref="Point" />.
    /// </summary>
    public class FeatureFactory : JsonConverterFactory {
        public override bool CanConvert(Type typeToConvert) {
            if (typeToConvert == typeof(Feature.Feature)) {
                return true;
            }

            var genericDefinition = typeToConvert.GetGenericTypeDefinition();

            if (genericDefinition == typeof(Feature<>)) {
                return true;
            }

            if (genericDefinition == typeof(Feature<,>)) {
                return true;
            }

            return false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) {
            var arguments = typeToConvert.GetGenericArguments();

            if (arguments.Length == 0) {
                return new FeatureConverter();
            }

            var geometryType = arguments[0];
            if (arguments.Length == 1) {
                return (JsonConverter)Activator.CreateInstance(typeof(GenericShapeFeatureConverter<>)
                .MakeGenericType(new Type[] { geometryType }),
                    BindingFlags.Instance | BindingFlags.Public,
                    binder: null,
                    args: new object[] { },
                    culture: null);
            }

            var propertyType = arguments[1];

            return (JsonConverter)Activator.CreateInstance(typeof(DoubleGenericFeatureConverter<,>)
                .MakeGenericType(new Type[] { geometryType, propertyType }),
                    BindingFlags.Instance | BindingFlags.Public,
                    binder: null,
                    args: new object[] { },
                    culture: null);
        }

        private class FeatureConverter : JsonConverter<Feature.Feature> {
            private static readonly GeometryConverter Converter = new GeometryConverter();
            private static readonly CrsConverter CrsConverter = new CrsConverter();

            /// <summary>
            ///     Reads the JSON representation of the object.
            /// </summary>
            /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
            /// <param name="objectType">Type of the object.</param>
            /// <param name="existingValue">The existing value of object being read.</param>
            /// <param name="serializer">The calling serializer.</param>
            /// <returns>
            ///     The object value.
            /// </returns>
            public override Feature.Feature Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options) {
                IGeometryObject geometry = null;
                ICRSObject crs = null;

                var properties = new Dictionary<string, object>();
                var id = string.Empty;

                if (reader.TokenType == JsonTokenType.EndObject) {
                    return new Feature.Feature(geometry, id){
                        CRS = crs
                    };
                }

                while (reader.Read()) {
                    if (reader.TokenType == JsonTokenType.EndObject) {
                        break;
                    }

                    if (reader.TokenType == JsonTokenType.PropertyName) {
                        var prop = reader.GetString();

                        switch (prop) {
                            case "id":
                                // advance to property
                                reader.Read();
                                id = reader.GetString();
                                break;
                            case "geometry":
                                // advance to array
                                reader.Read();

                                geometry = Converter.Read(ref reader, typeof(Feature.Feature), options);
                                break;
                            case "properties":
                                // advance to object
                                reader.Read();
                                while (reader.Read()) {
                                    if (reader.TokenType == JsonTokenType.EndObject) {
                                        break;
                                    }

                                    if (reader.TokenType != JsonTokenType.PropertyName) {
                                        throw new JsonException();
                                    }

                                    var key = reader.GetString();
                                    var value = JsonSerializer.Deserialize<object>(ref reader, options);

                                    properties.Add(key, value);
                                }
                                break;
                            case "crs":
                                crs = CrsConverter.Read(ref reader, typeof(ICRSObject), options);
                                break;
                            default:
                                reader.Read();
                                continue;
                        }
                    }
                }

                return new Feature.Feature(geometry, properties, id){
                    CRS = crs
                };
            }

            /// <summary>
            ///     Writes the JSON representation of the object.
            /// </summary>
            /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
            /// <param name="value">The value.</param>
            /// <param name="serializer">The calling serializer.</param>
            public override void Write(Utf8JsonWriter writer, Feature.Feature value, JsonSerializerOptions options) {
                writer.WriteStartObject();
                writer.WriteString("type", "Feature");

                if (!string.IsNullOrEmpty(value.Id)) {
                    writer.WriteString("id", value.Id);
                }

                writer.WritePropertyName("geometry");
                Converter.Write(writer, value.Geometry, options);

                writer.WritePropertyName("properties");
                JsonSerializer.Serialize(writer, value.Properties, options);

                if (value.CRS is not null) {
                    CrsConverter.Write(writer, value.CRS, options);
                }

                writer.WriteEndObject();
            }
        }

        private class GenericShapeFeatureConverter<TGeometry> : JsonConverter<Feature<TGeometry>>
            where TGeometry : IGeometryObject {

            private static readonly GeometryConverter Converter = new GeometryConverter();
            private static readonly CrsConverter CrsConverter = new CrsConverter();

            /// <summary>
            ///     Reads the JSON representation of the object.
            /// </summary>
            /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
            /// <param name="objectType">Type of the object.</param>
            /// <param name="existingValue">The existing value of object being read.</param>
            /// <param name="serializer">The calling serializer.</param>
            /// <returns>
            ///     The object value.
            /// </returns>
            public override Feature<TGeometry> Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options) {
                IGeometryObject geometry = null;
                ICRSObject crs = null;

                var properties = new Dictionary<string, object>();
                var id = string.Empty;

                if (reader.TokenType == JsonTokenType.EndObject) {
                    return new Feature<TGeometry>((TGeometry)geometry, properties){
                        CRS = crs
                    };
                }

                while (reader.Read()) {
                    if (reader.TokenType == JsonTokenType.EndObject) {
                        break;
                    }

                    if (reader.TokenType == JsonTokenType.PropertyName) {
                        var prop = reader.GetString();

                        switch (prop) {
                            case "id":
                                // advance to property
                                reader.Read();
                                id = reader.GetString();
                                break;
                            case "geometry":
                                // advance to array
                                reader.Read();
                                var geometryType = typeof(TGeometry);
                                if (geometryType == typeof(Point)) {
                                    geometry = new PointConverter().Read(ref reader, geometryType, options);
                                } else if (geometryType == typeof(MultiPoint)) {
                                    geometry = new MultiPointConverter().Read(ref reader, geometryType, options);
                                } else if (geometryType == typeof(LineString)) {
                                    geometry = new LineStringConverter().Read(ref reader, geometryType, options);
                                } else if (geometryType == typeof(MultiLineString)) {
                                    geometry = new MultiLineStringConverter().Read(ref reader, geometryType, options);
                                } else if (geometryType == typeof(Polygon)) {
                                    geometry = new PolygonConverter().Read(ref reader, geometryType, options);
                                } else if (geometryType == typeof(MultiPolygon)) {
                                    geometry = new MultiPolygonConverter().Read(ref reader, geometryType, options);
                                } else if (geometryType == typeof(GeometryCollection)) {
                                    geometry = new GeometryCollectionConverter().Read(ref reader, geometryType, options);
                                }
                                break;
                            case "properties":
                                // advance to object
                                reader.Read();
                                while (reader.Read()) {
                                    if (reader.TokenType == JsonTokenType.EndObject) {
                                        break;
                                    }

                                    if (reader.TokenType != JsonTokenType.PropertyName) {
                                        throw new JsonException();
                                    }

                                    var key = reader.GetString();
                                    var value = JsonSerializer.Deserialize<object>(ref reader, options);

                                    properties.Add(key, value);
                                }
                                break;
                            case "crs":
                                crs = CrsConverter.Read(ref reader, typeof(ICRSObject), options);
                                break;
                            default:
                                reader.Read();
                                continue;
                        }
                    }
                }

                return new Feature<TGeometry>((TGeometry)geometry, properties, id) {
                    CRS = crs
                };
            }

            /// <summary>
            ///     Writes the JSON representation of the object.
            /// </summary>
            /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
            /// <param name="value">The value.</param>
            /// <param name="serializer">The calling serializer.</param>
            public override void Write(Utf8JsonWriter writer, Feature<TGeometry> value, JsonSerializerOptions options) {
                writer.WriteStartObject();
                writer.WriteString("type", "Feature");

                if (!string.IsNullOrEmpty(value.Id)) {
                    writer.WriteString("id", value.Id);
                }

                writer.WritePropertyName("geometry");
                Converter.Write(writer, value.Geometry, options);

                writer.WritePropertyName("properties");
                JsonSerializer.Serialize(writer, value.Properties, options);

                if (value.CRS is not null) {
                    CrsConverter.Write(writer, value.CRS, options);
                }

                writer.WriteEndObject();
            }
        }

        private class DoubleGenericFeatureConverter<TGeometry, TProps> : JsonConverter<Feature<TGeometry, TProps>>
            where TGeometry : IGeometryObject {

            private static readonly GeometryConverter Converter = new GeometryConverter();
            private static readonly CrsConverter CrsConverter = new CrsConverter();

            /// <summary>
            ///     Reads the JSON representation of the object.
            /// </summary>
            /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
            /// <param name="objectType">Type of the object.</param>
            /// <param name="existingValue">The existing value of object being read.</param>
            /// <param name="serializer">The calling serializer.</param>
            /// <returns>
            ///     The object value.
            /// </returns>
            public override Feature.Feature<TGeometry, TProps> Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options) {
                IGeometryObject geometry = null;
                ICRSObject crs = null;
                var properties = Activator.CreateInstance<TProps>();
                var id = string.Empty;
                if (reader.TokenType == JsonTokenType.EndObject) {
                    return new Feature<TGeometry, TProps>((TGeometry)geometry, properties) {
                        CRS = crs
                    };
                }

                while (reader.Read()) {
                    if (reader.TokenType == JsonTokenType.EndObject) {
                        break;
                    }

                    if (reader.TokenType == JsonTokenType.PropertyName) {
                        var prop = reader.GetString();

                        switch (prop) {
                            case "id":
                                // advance to property
                                reader.Read();
                                id = reader.GetString();
                                break;
                            case "geometry":
                                // advance to array
                                reader.Read();

                                geometry = Converter.Read(ref reader, typeof(Feature.Feature), options);
                                break;
                            case "properties":
                                // advance to object
                                reader.Read();
                                properties = JsonSerializer.Deserialize<TProps>(ref reader, options);
                                break;
                            case "crs":
                                crs = CrsConverter.Read(ref reader, typeof(ICRSObject), options);
                                break;
                            default:
                                reader.Read();
                                continue;
                        }
                    }
                }

                return new Feature<TGeometry, TProps>((TGeometry)geometry, properties, id) {
                    CRS = crs
                };
            }

            /// <summary>
            ///     Writes the JSON representation of the object.
            /// </summary>
            /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
            /// <param name="value">The value.</param>
            /// <param name="serializer">The calling serializer.</param>
            public override void Write(Utf8JsonWriter writer, Feature.Feature<TGeometry, TProps> value, JsonSerializerOptions options) {
                writer.WriteStartObject();
                writer.WriteString("type", "Feature");

                if (!string.IsNullOrEmpty(value.Id)) {
                    writer.WriteString("id", value.Id);
                }

                writer.WritePropertyName("geometry");
                Converter.Write(writer, value.Geometry, options);

                writer.WritePropertyName("properties");
                JsonSerializer.Serialize(writer, value.Properties, options);

                if (value.CRS is not null) {
                    CrsConverter.Write(writer, value.CRS, options);
                }

                writer.WriteEndObject();
            }
        }
    }
}
