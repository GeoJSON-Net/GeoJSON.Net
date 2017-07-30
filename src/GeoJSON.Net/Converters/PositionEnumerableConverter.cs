// Copyright © Joerg Battermann 2014, Matt Hunt 2017

using System;
#if (!NET35 || !NET40)
using System.Reflection;
#endif
using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeoJSON.Net.Converters
{
    /// <summary>
    /// Converter to read and write the <see cref="IEnumerable{IPosition}" /> type.
    /// </summary>
    public class PositionEnumerableConverter : JsonConverter
    {
        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            #if (NET35 || NET40)
            return typeof(IEnumerable<IPosition>).IsAssignableFrom(objectType);
#else
			return typeof(IEnumerable<IPosition>).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
#endif
        }

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
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            double[][] coordinates = existingValue == null
                ? serializer.Deserialize<double[][]>(reader)
                : (double[][])existingValue;

            IList<IPosition> positions = new List<IPosition>(coordinates.Length);

            foreach (var coordinate in coordinates)
            {
                positions.Add(coordinate.Length == 3
                    ? new Position(coordinate[1], coordinate[0], coordinate[2])
                    : new Position(coordinate[1], coordinate[0]));
            }

            return positions;
        }

        /// <summary>
        ///     Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IReadOnlyList<IPosition> coordinateElements)
            {
                JArray coordinateArray = new JArray();
                foreach (var position in coordinateElements)
                {
                    JArray coordinateElement = position.Altitude.HasValue 
                        ? new JArray(position.Longitude, position.Latitude, position.Altitude)
                        : new JArray(position.Longitude, position.Latitude);

                    coordinateArray.Add(coordinateElement);
                }

                serializer.Serialize(writer, coordinateArray);
            }
            else
            {
                throw new ArgumentException($"{nameof(PositionEnumerableConverter)}: unsupported value type");
            }
        }
    }
}