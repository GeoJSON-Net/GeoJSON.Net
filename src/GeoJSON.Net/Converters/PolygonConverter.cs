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

            var listOfLines = new List<LineString>();

            //parsing coordinates groups

            int BeginBracket = 0;
            for (int i = 0; i < inputJsonValue.Count(); i++)
            {
                if (inputJsonValue[i] == ']' && inputJsonValue[i + 1] == ']')
                {
                    // this is the end of a polygon
                    LineString test = getLineStringMatches(inputJsonValue.Substring(BeginBracket, i - BeginBracket+1));
                    if (test != null)
                    {
                        listOfLines.Add(test);
                    }

                    BeginBracket = i; // reset begin bracket

                    // after we have converted this polygon, if the third next character is a square bracket this is the end of the polygon
                    if (inputJsonValue[i + 2] == ']')
                    {
                        // this is the end of the entire polygon, break
                        break; // this will stop the last polygon from being registered twice
                    }
                }
            }
            return listOfLines;
        }

        private LineString getLineStringMatches(string inputJsonString)
        {
            var polygonCoordinates = new List<GeographicPosition>();
            MatchCollection coordinateGroups = Regex.Matches(inputJsonString, @"(\[[-+]{0,1}\d{1,3}.\d+,[-+]{0,1}\d{1,2}.\d+[,\d+.\d+]*\])");
            foreach (Match coordinatePair in coordinateGroups)
            {
                var coordinates = Regex.Match(coordinatePair.Value, @"(?<longitude>[+-]{0,1}\d+.\d+),(?<latitude>[+-]{0,1}\d+.\d+)(?:,)?(?<altitude>\d+.\d+)*");

                double lng;
                double lat;
                double alt;

                double.TryParse(coordinates.Groups["longitude"].Value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out lng);
                double.TryParse(coordinates.Groups["latitude"].Value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out lat);
                double.TryParse(coordinates.Groups["altitude"].Value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out alt);

                if (lng != 0 && lat != 0)
                    if (alt == 0)
                        polygonCoordinates.Add(new GeographicPosition(lat, lng));
                    else
                        polygonCoordinates.Add(new GeographicPosition(lat, lng, alt));

            }

            if (polygonCoordinates.Count() < 4)
            {
                return null;
            }

            LineString output = new LineString(polygonCoordinates.ToList<IPosition>());

            if(! output.IsLinearRing())
            {
                if (output.Coordinates.Count() < 4)
                {
                    return null;
                }
                else
                {
                    // set the final coordinate to the first coordinates
                    polygonCoordinates.Add(polygonCoordinates[0]);
                    return new LineString(polygonCoordinates.ToList<IPosition>());
                }
            }


            return new LineString(polygonCoordinates.ToList<IPosition>());
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
