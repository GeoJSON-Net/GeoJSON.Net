// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PolygonConverter.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the PolygonConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Converters
{
    using System;

    using GeoJSON.Net.Geometry;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Globalization;

    /// <summary>
    /// Converter to read and write the <see cref="MultiPolygon" /> type.
    /// </summary>
    public class PolygonConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param><param name="value">The value.</param><param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var coordinateElements = value as System.Collections.Generic.List<GeoJSON.Net.Geometry.LineString>;
            if (coordinateElements != null && coordinateElements.Count > 0)
            {
                var coordinateArray = new JArray();
                if (coordinateElements[0].Coordinates[0] is GeographicPosition)
                {
                    foreach(var subPolygon in coordinateElements)
                    {
                        var subPolygonCoordinateArray = new JArray();
                        foreach (var coordinates in subPolygon.Coordinates.Select(x => x as GeographicPosition))
                        {
                            var coordinateElement = new JArray(coordinates.Longitude, coordinates.Latitude);
                            if (coordinates.Altitude.HasValue && coordinates.Altitude != 0)
                                coordinateElement = new JArray(coordinates.Longitude, coordinates.Latitude, coordinates.Altitude);

                            subPolygonCoordinateArray.Add(coordinateElement);
                        }
                        coordinateArray.Add(subPolygonCoordinateArray);
                    }
                    serializer.Serialize(writer, coordinateArray);
                }
                else
                    // ToDo: implement
                    throw new NotImplementedException();
            }
            else
                serializer.Serialize(writer, value);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param><param name="objectType">Type of the object.</param><param name="existingValue">The existing value of object being read.</param><param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var inputJsonValue = serializer.Deserialize(reader).ToString();

            //sanitizing input
            inputJsonValue = inputJsonValue.Replace(Environment.NewLine, "");
            inputJsonValue = inputJsonValue.Replace(" ", "");

            var polygonCoordinates = new List<GeographicPosition>();

            //parsing coordinates groups
            MatchCollection coordinateGroups = Regex.Matches(inputJsonValue, @"(\[\d+.\d+,\d+.\d+[,\d+.\d+]*\])");
            foreach (Match coordinatePair in coordinateGroups) 
            {
                var coordinates = Regex.Match(coordinatePair.Value, @"(?<longitude>\d+.\d+),(?<latitude>\d+.\d+)(?:,)?(?<altitude>\d+.\d+)*");

                double lng;
                double lat;
                double alt;

                double.TryParse(coordinates.Groups["longitude"].Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out lng);
                double.TryParse(coordinates.Groups["latitude"].Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out lat);
                double.TryParse(coordinates.Groups["altitude"].Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out alt);

                if (lng != 0 && lat != 0)
                    if (alt == 0)
                        polygonCoordinates.Add(new GeographicPosition(lat, lng));
                    else
                        polygonCoordinates.Add(new GeographicPosition(lat, lng, alt));
                
            }


            return new List<LineString> { new LineString(polygonCoordinates.ToList<IPosition>()) };
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Polygon);
        }
    }
}
