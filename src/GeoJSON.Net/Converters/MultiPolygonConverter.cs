// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System;
using GeoJSON.Net.Geometry;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using GeoJSON.Net.CoordinateReferenceSystem;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    ///     Converter to read and write an <see cref="IPosition" />, that is,
    ///     the coordinates of a <see cref="Point" />.
    /// </summary>
    public class MultiPolygonConverter : JsonConverter<MultiPolygon>
    {
        private static readonly PolygonEnumerableConverter Converter = new PolygonEnumerableConverter();
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
        public override MultiPolygon Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            MultiPolygon geometry = null;
            ICRSObject crs = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject) {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    continue;
                }

                var propertyName = reader.GetString();

                switch (propertyName) {
                    case "coordinates":
                        // advance to coordinates
                        reader.Read();

                        // must real all json. cannot exit early
                        geometry = new MultiPolygon(Converter.Read(ref reader, typeof(IEnumerable<Polygon>), options));
                        break;
                    case "crs":
                        crs = CrsConverter.Read(ref reader, typeof(ICRSObject), options);
                        break;
                }
           }

            geometry.CRS = crs;

            return geometry;
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void Write(Utf8JsonWriter writer, MultiPolygon value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", "MultiPolygon");
            writer.WritePropertyName("coordinates");
            Converter.Write(writer, value.Polygons, options);
            if (value.CRS is not null) {
                CrsConverter.Write(writer, value.CRS, options);
            }
            writer.WriteEndObject();
        }
    }
}
