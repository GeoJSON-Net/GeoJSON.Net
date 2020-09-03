// Copyright � Joerg Battermann 2014, Matt Hunt 2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using GeoJSON.Net.Geometry;


namespace GeoJSON.Net.Converters
{
    /// <summary>
    /// Converter to read and write the <see cref="IEnumerable{Point}" /> type.
    /// </summary>
    public class PointEnumerableConverter : JsonConverter<IEnumerable<Point>>
    {
        private static readonly PositionConverter PositionConverter = new PositionConverter();
        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, IEnumerable<Point> points, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var point in points)
            {
                PositionConverter.Write(writer, point.Coordinates, options);
            }
            writer.WriteEndArray();
        }

        /// <inheritdoc />
        public override IEnumerable<Point> Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            var coordinates = JsonSerializer.Deserialize<IEnumerable<Position>>(ref reader, options);
            return coordinates.Select(position => new Point(position));
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IEnumerable<Point>);
        }
    }
}
