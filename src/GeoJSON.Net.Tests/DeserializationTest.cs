using GeoJSON.Net.Feature;
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
        public void FeatureCollectionDeserialization()
        {
            var geoJsonText = @"{'type': 'FeatureCollection', 'crs': {'type': 'name','properties': {'name': 'urn:ogc:def:crs:OGC:1.3:CRS84'}},
                'features': [{'type': 'Feature','properties': {'ITEM_CODE': 'PB','UNIQUE_ID': '1570',},'geometry': {'type': 'Polygon', 'coordinates': [[
                [-0.12513, 51.542634],[-0.125125,51.542618],[-0.125279,51.542595],[-0.125362,51.542583],[-0.125369,51.542601],[-0.12513,51.542634]]]}}]}";

            var featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(geoJsonText,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            Assert.IsNotNull(featureCollection);
            Assert.AreEqual(1, featureCollection.Features.Count);
        }
    }
}
