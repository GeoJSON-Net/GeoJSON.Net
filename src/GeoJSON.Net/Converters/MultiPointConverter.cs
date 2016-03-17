using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Exceptions;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class MultiPointConverter : JsonConverter
    {
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
                    double?[] m = null;

                    if (coordinate.Length == 3)
                    {
                        altitude = coordinate[2];
                    }
                    if (coordinate.Length > 3)
                    {
                        m = new double?[coordinate.Length - 3];
                        int mIndex = 0;
                        for (int i = 3; i < coordinate.Length; i++)
                        {
                            m[mIndex] = coordinate[i];
                            mIndex++;
                        }
                    }

                    positions.Add(new Point(new GeographicPosition(latitude, longitude, altitude,m)));
                }

                return positions;
            }
            catch (Exception ex)
            {
                throw new ParsingException("Could not parse GeoJSON Response. (Latitude or Longitude missing from Point geometry?)", ex);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MultiPoint);
        }
    }
}