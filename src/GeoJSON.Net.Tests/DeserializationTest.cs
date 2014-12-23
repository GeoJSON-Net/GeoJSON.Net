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
		public void MultiPolygonDeserialization()
		{
			#region geoJsonText

			var geoJsonText = @"{'coordinates':[[[
  [
    -2.6797102391514338,
    52.959676831105995
  ],
  [
    -2.6769029474483279,
    52.9608756693609
  ],
  [
    -2.6079763270327119,
    52.908449372833715
  ],
  [
    -2.5815104708998668,
    52.891287242948195
  ],
  [
    -2.5851645010668989,
    52.875476700983896
  ],
  [
    -2.6050779098387191,
    52.882954723868622
  ],
  [
    -2.6373482332006359,
    52.875255907042678
  ],
  [
    -2.6932445076063951,
    52.878791122091066
  ],
  [
    -2.693133462937789,
    52.89564268523565
  ],
  [
    -2.6548779332193022,
    52.930592009390175
  ],
  [
    -2.6797102391514338,
    52.959676831105995
  ]
]],[[
  [
    -2.69628632041613,
    52.89610842810761
  ],
  [
    -2.75901233808515,
    52.8894641454077
  ],
  [
    -2.7663172788742449,
    52.89938894657412
  ],
  [
    -2.804554822840895,
    52.90253773227807
  ],
  [
    -2.83848602260174,
    52.929801009654575
  ],
  [
    -2.838979264607087,
    52.94013913205788
  ],
  [
    -2.7978187468478741,
    52.937353122653533
  ],
  [
    -2.772273870352612,
    52.920394929466184
  ],
  [
    -2.6996509024137052,
    52.926572918779222
  ],
  [
    -2.69628632041613,
    52.89610842810761
  ]
]]],'type':'MultiPolygon'}";

			#endregion

			var multipolygon = JsonConvert.DeserializeObject<MultiPolygon>(geoJsonText,
				new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

			Assert.IsNotNull(multipolygon);
			Assert.IsNotNull(multipolygon.Coordinates);
			Assert.IsTrue(multipolygon.Coordinates.Count == 2);
			Assert.IsTrue(multipolygon.Coordinates[0].Coordinates.Count == 1);
			Assert.IsTrue(multipolygon.Coordinates[0].Coordinates[0].Coordinates.Count == 11);
			var firstPolygon = multipolygon.Coordinates.First();
			var firstPoint = firstPolygon.Coordinates[0].Coordinates[0] as GeographicPosition;
			Assert.IsTrue(Math.Abs(firstPoint.Latitude - 52.959676831105995) < 0.0001);
			Assert.IsTrue(Math.Abs(firstPoint.Longitude - -2.6797102391514338) < 0.0001);

			var lastPolygon = multipolygon.Coordinates.Last();
			var lastPoint = lastPolygon.Coordinates.Last().Coordinates.Last() as GeographicPosition;
			Assert.IsTrue(Math.Abs(lastPoint.Latitude - 52.89610842810761) < 0.0001);
			Assert.IsTrue(Math.Abs(lastPoint.Longitude - -2.69628632041613) < 0.0001);
		}

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
