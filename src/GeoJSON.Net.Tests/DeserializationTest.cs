using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Drawing;
using NUnit.Framework;

namespace GeoJSON.Net.Tests
{
    [TestFixture]
    public class DeserializationTest
    {
		[TestFixture]
		public class DeserializationOfFeatureTest
		{
			[Test]
			public void CanDeserializeFeature()
			{
				#region GeoJSON
				const string geoJsonText = @"{
  'type': 'Feature',
  'id' : 'test-id',
  'geometry': {
    'type': 'Point',
    'coordinates': [125.6, 10.1]
  },
  'properties': {
    'name': 'Dinagat Islands'
  }
}";
				#endregion

				var feature = JsonConvert.DeserializeObject<Feature.Feature>(geoJsonText);

				Assert.IsNotNull(feature);
				Assert.IsNotNull(feature.Properties);
				Assert.IsTrue(feature.Properties.Any());

				Assert.IsNotNull(feature.Properties["name"]);
				Assert.AreEqual(feature.Properties["name"], "Dinagat Islands");

				Assert.AreEqual(feature.Id, "test-id");

				Assert.AreEqual(feature.Geometry.Type, GeoJSONObjectType.Point);
			}
		}

		[TestFixture]
		public class DeserializationOfFeatureCollectionTest
		{
			[Test]
			public void CanDeserializeFeatureCollection()
			{
				#region GeoJSON
				const string geoJsonText = @"
{  
   'type':'FeatureCollection',
   'features':[  
      {  
         'type':'Feature',
         'geometry':{  
            'type':'Point',
            'coordinates':[ 102.0, 0.5 ]
         },
         'properties':{  
            'prop0':'value0'
         }
      },
	  {
		'type':'Feature',
		'properties':{'name':'DD'},
		'geometry': {
			'type':'MultiPolygon',
			'coordinates':[[[[-3.124469107867639,56.43179349026641],[-3.181864056758185,56.50435867827879],[-3.080807472497396,56.58041883184697],[-3.204635351704243,56.66878970099241],[-3.153385207792676,56.750141153246226],[-3.300369428804113,56.8589226202768],[-3.20971234483721,56.947300739465064],[-3.064462793503021,56.91976858406769],[-2.972112587880359,56.97746168167823],[-2.854882511931398,56.98360267279684],[-2.680251743133697,56.945352112881636],[-2.615357138064907,56.78566372854147],[-2.493780338741513,56.76540172907848],[-2.315459650038894,56.87577071411662],[-2.224180437247053,56.88745481725907],[-2.309193985939006,56.80497206404891],[-2.410860986028102,56.768333064132314],[-2.551721986204847,56.560417064546556],[-2.719166986355991,56.49336106469278],[-3.124469107867639,56.43179349026641]]],[[[-2.818223720652239,56.423668560365314],[-2.975782222542367,56.380750980197035],[-3.063948244048636,56.392897691447075],[-2.921693986527472,56.452056064793695],[-2.818223720652239,56.423668560365314]]]]
		}
	  },
      {  
         'type':'Feature',
         'geometry':{  
            'type':'Polygon',
            'coordinates':[ [ [ 100.0, 0.0 ], [ 101.0, 0.0 ], [ 101.0, 1.0 ], [ 100.0, 1.0 ], [ 100.0, 0.0 ] ] ]
         },
         'properties':{  
            'prop0':'value0',
            'prop1':{ 'this':'that' }
         }
      }
   ]
}";
				#endregion

				var featureCollection = JsonConvert.DeserializeObject<Feature.FeatureCollection>(geoJsonText);

				Assert.IsNotNull(featureCollection.Features);
				Assert.AreEqual(featureCollection.Features.Count, 3);
				Assert.AreEqual(featureCollection.Features.Count(x => x.Geometry.Type == GeoJSONObjectType.Point), 1);
				Assert.AreEqual(featureCollection.Features.Count(x => x.Geometry.Type == GeoJSONObjectType.MultiPolygon), 1);
				Assert.AreEqual(featureCollection.Features.Count(x => x.Geometry.Type == GeoJSONObjectType.Polygon), 1);
			}
		}

		[Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
        public void FeatureCollectionDeserialization()
        {
            const string geoJsonText = @"{'type': 'FeatureCollection', 'crs': {'type': 'name','properties': {'name': 'urn:ogc:def:crs:OGC:1.3:CRS84'}},
                'features': [{'type': 'Feature','properties': {'ITEM_CODE': 'PB','UNIQUE_ID': '1570',},'geometry': {'type': 'Polygon', 'coordinates': [[
                [-0.12513, 51.542634],[-0.125125,51.542618],[-0.125279,51.542595],[-0.125362,51.542583],[-0.125369,51.542601],[-0.12513,51.542634]]]}}]}";

            var featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(geoJsonText,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            Assert.IsNotNull(featureCollection);
            Assert.AreEqual(1, featureCollection.Features.Count);
        }
    }
}

