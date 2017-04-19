using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Exceptions;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    /// Converter to read and write the <see cref="MultiPoint" /> type.
    /// </summary>
    public class MultiPointConverter : JsonConverter
    {
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var points = (List<Point>)value;
            if (points.Any())
            {
                var converter = new PointConverter();

                writer.WriteStartArray();

                foreach (var point in points)
                {
                    converter.WriteJson(writer, point.Coordinates, serializer);
                }

                writer.WriteEndArray();
            }
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var coordinates = serializer.Deserialize<double[][]>(reader);
            var positions = new List<Point>(coordinates.Length);
            try
            {
                foreach (var coordinate in coordinates)
                {
                    var longitude = coordinate[0];
                    var latitude = coordinate[1];
                    double? altitude = null;

                    if (coordinate.Length == 3)
                    {
                        altitude = coordinate[2];
                    }

                    positions.Add(new Point(new Position(latitude, longitude, altitude)));
                }

                return positions;
            }
            catch (Exception ex)
            {
                throw new ParsingException("Could not parse GeoJSON Response. (Latitude or Longitude missing from Point geometry?)", ex);
            }
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MultiPoint);
        }
    }
}