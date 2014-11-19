using GeoJSON.Net.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoJSON.Net.Tests
{
    [TestClass]
    public class DeserializationTest
    {
        [TestMethod]
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

            Assert.IsNotNull(polygon);
            Assert.IsNotNull(polygon.Coordinates);
            Assert.IsTrue(polygon.Coordinates.Count == 1);
            Assert.IsTrue(polygon.Coordinates[0].Coordinates.Count == 4);

            var firstPoint = polygon.Coordinates[0].Coordinates[0] as GeographicPosition;
            Assert.IsTrue(Math.Abs(firstPoint.Latitude - 52.37979082) < 0.0001);
            Assert.IsTrue(Math.Abs(firstPoint.Longitude - 5.3173828125) < 0.0001);
            Assert.IsTrue(!firstPoint.Altitude.HasValue);

            var thirdPoint = polygon.Coordinates[0].Coordinates[2] as GeographicPosition;
            Assert.IsTrue(thirdPoint.Altitude.HasValue && Math.Abs(thirdPoint.Altitude.Value - 4.23) < 0.0001);

        }

        [TestMethod]
        public void PolygonDeserialization1()
        {
            #region geoJsonText
            var geoJsonText = @"{
        'type': 'Polygon',
        'coordinates': [
          [
            [
              165.3173828125,
              -52.379790828551016
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
              165.3173828125,
              -52.379790828551016
            ]
          ]
        ]
      }";
            #endregion
            var polygon = JsonConvert.DeserializeObject<Polygon>(geoJsonText, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            var firstPoint = polygon.Coordinates[0].Coordinates[0] as GeographicPosition;
            Assert.IsTrue(Math.Abs(firstPoint.Latitude + 52.37979082) < 0.0001);
            Assert.IsTrue(Math.Abs(firstPoint.Longitude - 165.3173828125) < 0.0001);
            Assert.IsTrue(!firstPoint.Altitude.HasValue);
        }

        [TestMethod]
        public void PointDeserialization()
        {
            #region data
            var geoJsonText = @"{
        'type': 'Point',
        'coordinates': 
            [
              165.3173828125,
              -52.379790828551016
            ]
      }";
            #endregion
            var point = JsonConvert.DeserializeObject<Point>(geoJsonText, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            var coordinates = point.Coordinates as GeographicPosition;

            Assert.IsTrue(coordinates.Longitude - 165.3173828125 < 0.0001);
            Assert.IsTrue(coordinates.Latitude + 52.379790828551016 < 0.0001);
        }

        [TestMethod]
        public void MultiLineStringDeserialization()
        {
            #region geoJsonText
            var geoJsonText = @"{
        'type': 'MultiLineString',
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
            ]
          ],
          [
            [
              5.3273828125,
              52.379790828551016
              
            ],
            [
              5.486085205078125,
              52.36721467920585
            ],
            [
              5.426047363281249,
              52.303440474272755,
                4.23
            ]
          ]
        ]
      }";
            #endregion
            //geoJsonText = geoJsonText.Replace("\r\n", "");
            var multiLineString = JsonConvert.DeserializeObject<MultiLineString>(geoJsonText, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            Assert.IsNotNull(multiLineString);
            Assert.IsNotNull(multiLineString.Coordinates);
            Assert.AreEqual(2, multiLineString.Coordinates.Count);
            Assert.AreEqual(3, multiLineString.Coordinates[0].Coordinates.Count);
            Assert.AreEqual(3, multiLineString.Coordinates[1].Coordinates.Count);

            var firstPoint = multiLineString.Coordinates[0].Coordinates[0] as GeographicPosition;
            Assert.IsNotNull(firstPoint);
            Assert.AreEqual(52.37979082, firstPoint.Latitude, 0.0001);
            Assert.AreEqual(5.3173828125, firstPoint.Longitude, 0.0001);
            Assert.IsTrue(!firstPoint.Altitude.HasValue);

            var thirdPoint = multiLineString.Coordinates[0].Coordinates[2] as GeographicPosition;
            Assert.IsNotNull(thirdPoint);
            Assert.IsTrue(thirdPoint.Altitude.HasValue);
            Assert.AreEqual(4.23, thirdPoint.Altitude.Value, 0.0001);

        }
    }
}
