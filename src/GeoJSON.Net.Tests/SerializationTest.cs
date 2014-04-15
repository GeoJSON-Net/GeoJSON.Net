using GeoJSON.Net.Converters;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;
using System.Text.RegularExpressions;

namespace GeoJSON.Net.Tests
{
    public class SerializationTest
    {
        [Fact]
        public void PolygonSerialization()
        {
            var coordinates = new List<GeographicPosition> 
                { 
                    new GeographicPosition(52.370725881211314, 4.889259338378906), 
                    new GeographicPosition(52.3711451105601, 4.895267486572266), 
                    new GeographicPosition(52.36931095278263, 4.892091751098633), 
                    new GeographicPosition(52.370725881211314, 4.889259338378906) 
                }.ToList<IPosition>();

            var model = new Polygon(new List<LineString> { new LineString(coordinates) });
            var serializedData = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            var matches = Regex.Matches(serializedData, @"(?<coordinates>[0-9]+([.,][0-9]+))");

            double lng;
            double.TryParse(matches[0].Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out lng);

            //Double precision can pose a problem 
            Assert.True(Math.Abs(lng - 4.889259338378906) < 0.0000001);

            Assert.True(!serializedData.Contains("latitude"));
        }

        [Fact]
        public void PolygonDeserialization()
        {
            #region geoJsonText
            var geoJsonText = @"{
        'type': 'Polygon',
        'coordinates': [
          [
            [
              5.3173828125,
              52.379790828551016
              
            ],
            [
              5.456085205078125,
              52.36721467920585
            ],
            [
              5.386047363281249,
              52.303440474272755,
                4.23
            ],
            [
              5.3173828125,
              52.379790828551016
            ]
          ]
        ]
      }";
            #endregion
            //geoJsonText = geoJsonText.Replace("\r\n", "");
            var polygon = JsonConvert.DeserializeObject<Polygon>(geoJsonText, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            Assert.NotNull(polygon);
            Assert.NotNull(polygon.Coordinates);
            Assert.True(polygon.Coordinates.Count == 1);
            Assert.True(polygon.Coordinates[0].Coordinates.Count == 4);

            var firstPoint = polygon.Coordinates[0].Coordinates[0] as GeographicPosition;
            Assert.True(Math.Abs(firstPoint.Latitude - 52.37979082) < 0.0001);
            Assert.True(Math.Abs(firstPoint.Longitude - 5.3173828125) < 0.0001);
            Assert.True(!firstPoint.Altitude.HasValue);

            var thirdPoint = polygon.Coordinates[0].Coordinates[2] as GeographicPosition;
            Assert.True(thirdPoint.Altitude.HasValue && Math.Abs(thirdPoint.Altitude.Value - 4.23) < 0.0001);

        }
    }
}
