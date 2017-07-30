// Copyright � Joerg Battermann 2014, Matt Hunt 2017

using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Exceptions;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    /// Converter to read and write the <see cref="IEnumerable{Point}" /> type.
    /// </summary>
    public class PointEnumerableConverter : JsonConverter
    {
        private static readonly PointCoordinatesConverter PositionConverter = new PointCoordinatesConverter();
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IEnumerable<Point> points)
            {
                writer.WriteStartArray();
                foreach (var point in points)
                {
                    PositionConverter.WriteJson(writer, point.Coordinates, serializer);
                }
                writer.WriteEndArray();
            }
            else
            {
                throw new ArgumentException($"{nameof(PointEnumerableConverter)}: unsupported value {value}");
            }
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var coordinates = serializer.Deserialize<double[][]>(reader);
            return coordinates.Select(position => new Point(position.ToPosition()));
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IEnumerable<Point>);
        }
    }
}