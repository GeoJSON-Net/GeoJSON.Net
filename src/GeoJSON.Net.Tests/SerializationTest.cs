using GeoJSON.Net.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;

namespace GeoJSON.Net.Tests
{
    [TestClass]
    public class SerializationTest
    {
        /// <summary>
        /// Serializes the whole Polygon with properties
        /// </summary>
        [TestMethod]
        public void PointFeatureSerialization() 
        {
            var point = new GeoJSON.Net.Geometry.Point(new GeoJSON.Net.Geometry.GeographicPosition(45.79012, 15.94107));
            var featureProperties = new Dictionary<string, object> { {"Name", "Foo"} };
            var model = new GeoJSON.Net.Feature.Feature(point, featureProperties);
            var serializedData = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore });

            Assert.IsFalse(serializedData.Contains("longitude"));
        }

        [TestMethod]
        public void PolygonFeatureSerialization() 
        {
            var coordinates = new List<GeographicPosition> 
                { 
                    new GeographicPosition(52.370725881211314, 4.889259338378906), 
                    new GeographicPosition(52.3711451105601, 4.895267486572266), 
                    new GeographicPosition(52.36931095278263, 4.892091751098633), 
                    new GeographicPosition(52.370725881211314, 4.889259338378906) 
                }.ToList<IPosition>();

            var polygon = new Polygon(new List<LineString> { new LineString(coordinates) });
            var featureProperties = new Dictionary<string, object> { { "Name", "Foo" } };
            var model = new GeoJSON.Net.Feature.Feature(polygon, featureProperties);

            var serializedData = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore });
        }


        [TestMethod]
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
            var serializedData = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore });

            var matches = Regex.Matches(serializedData, @"(?<coordinates>[0-9]+([.,][0-9]+))");

            double lng;
            double.TryParse(matches[0].Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out lng);

            //Double precision can pose a problem 
            Assert.IsTrue(Math.Abs(lng - 4.889259338378906) < 0.0000001);

            Assert.IsTrue(!serializedData.Contains("latitude"));
        }

        [TestMethod]
        public void MultiLineStringSerialization()
        {
            var coordinates = new[]
            {
                new List<IPosition> 
                { 
                    new GeographicPosition(52.370725881211314, 4.889259338378906), 
                    new GeographicPosition(52.3711451105601, 4.895267486572266), 
                    new GeographicPosition(52.36931095278263, 4.892091751098633), 
                    new GeographicPosition(52.370725881211314, 4.889259338378906) 
                },
                new List<IPosition> 
                { 
                    new GeographicPosition(52.370725881211314, 4.989259338378906), 
                    new GeographicPosition(52.3711451105601, 4.995267486572266), 
                    new GeographicPosition(52.36931095278263, 4.992091751098633), 
                    new GeographicPosition(52.370725881211314, 4.989259338378906) 
                },
            };

            var model = new MultiLineString(coordinates.Select(ca => new LineString(ca)).ToList());
            var serializedData = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore });

            var matches = Regex.Matches(serializedData, @"(?<coordinates>[0-9]+([.,][0-9]+))");

            double lng;
            double.TryParse(matches[0].Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out lng);

            //Double precision can pose a problem 
            Assert.IsTrue(Math.Abs(lng - 4.889259338378906) < 0.0000001);

            Assert.IsTrue(!serializedData.Contains("latitude"));
        }

        [TestMethod]
        public void GeographicPositionSerialization()
        {
            var model = new GeoJSON.Net.Geometry.GeographicPosition(112.12, 10);

            var serialized = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            var matches = Regex.Matches(serialized, @"(\d+.\d+)");
            Assert.IsTrue(matches.Count == 2);
            double lng= 0;
            double.TryParse(matches[0].Value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out lng);

            Assert.AreEqual(lng, 112.12);
        }

        [TestMethod]
        public void FeatureSerialization()
        {
            var coordinates = new[]
            {
                new List<IPosition> 
                { 
                    new GeographicPosition(52.370725881211314, 4.889259338378906), 
                    new GeographicPosition(52.3711451105601, 4.895267486572266), 
                    new GeographicPosition(52.36931095278263, 4.892091751098633), 
                    new GeographicPosition(52.370725881211314, 4.889259338378906) 
                },
                new List<IPosition> 
                { 
                    new GeographicPosition(52.370725881211314, 4.989259338378906), 
                    new GeographicPosition(52.3711451105601, 4.995267486572266), 
                    new GeographicPosition(52.36931095278263, 4.992091751098633), 
                    new GeographicPosition(52.370725881211314, 4.989259338378906) 
                },
            };
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            IGeometryObject geometry;

            geometry = new LineString(coordinates[0]);
            AssertCoordinates(JsonConvert.SerializeObject(new Feature.Feature(geometry), settings), 1, coordinates[0]);
            geometry = new Point(coordinates[0][0]);
            AssertCoordinates(JsonConvert.SerializeObject(new Feature.Feature(geometry), settings), 0, coordinates[0].Take(1).ToArray());
            geometry = new MultiLineString(coordinates.Select(ca => new LineString(ca)).ToList());
            AssertCoordinates(JsonConvert.SerializeObject(new Feature.Feature(geometry), settings), 2, coordinates);
            geometry = new Polygon(coordinates.Select(ca => new LineString(ca)).ToList());
            AssertCoordinates(JsonConvert.SerializeObject(new Feature.Feature(geometry), settings), 2, coordinates);
        }

        [TestMethod]
        public void MultiPolygonSerialization()
        {
            var expectedJson = "{\"geometry\":{\"coordinates\":[[[[0.0,0.0],[1.0,0.0],[1.0,1.0],[0.0,1.0],[0.0,0.0]]],[[[100.0,100.0],[101.0,100.0],[101.0,101.0],[100.0,101.0],[100.0,100.0]],[[200.0,200.0],[201.0,200.0],[201.0,201.0],[200.0,201.0],[200.0,200.0]]]],\"type\":\"MultiPolygon\"},\"properties\":{},\"type\":\"Feature\"}";
            var polygon1 = new Polygon(new List<LineString>
            {
                new LineString((new List<GeographicPosition>
                {
                    new GeographicPosition(0, 0),
                    new GeographicPosition(0, 1),
                    new GeographicPosition(1, 1),
                    new GeographicPosition(1, 0),
                    new GeographicPosition(0, 0)
                }).ToList<IPosition>())

            });

            var polygon2 = new Polygon(new List<LineString>
            {
                new LineString((new List<GeographicPosition>
                {
                    new GeographicPosition(100, 100),
                    new GeographicPosition(100, 101),
                    new GeographicPosition(101, 101),
                    new GeographicPosition(101, 100),
                    new GeographicPosition(100, 100)
                }).ToList<IPosition>()),
                new LineString((new List<GeographicPosition>
                {
                    new GeographicPosition(200, 200),
                    new GeographicPosition(200, 201),
                    new GeographicPosition(201, 201),
                    new GeographicPosition(201, 200),
                    new GeographicPosition(200, 200)
                }).ToList<IPosition>())

            });

            var multipolygon = new MultiPolygon(new List<Polygon> { polygon1, polygon2 });
            var newFeature = new Feature.Feature(multipolygon);
            var serializedData = JsonConvert.SerializeObject(newFeature, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore });
            var serializedDataWithouWhiteSpace = Regex.Replace(serializedData, @"(\s|$)+", "");
            Assert.IsTrue(serializedDataWithouWhiteSpace == expectedJson);
        }

        private void AssertCoordinates(string geojson, int expectedNesting, IEnumerable<object> coords)
        {
            var coordMatch = Regex.Matches(geojson, "\"coordinates\":(.+?)(,\\s*\"|})");
            Assert.AreEqual(1, coordMatch.Count);
            var deserializedCoords = JsonConvert.DeserializeObject<JArray>(coordMatch[0].Groups[1].Value);
            AssertCoordInternal(deserializedCoords, expectedNesting, coords);
        }

        private void AssertCoordInternal(JArray coords, int expectedNesting, IEnumerable<object> expectedCoords)
        {
            Assert.IsTrue(expectedNesting >= 0);

            if (expectedNesting == 0)
            {
                AssertCoordinate((GeographicPosition) expectedCoords.First(), coords);
            }
            else
            {
                var enumerator = expectedCoords.GetEnumerator();
                var moveNext = enumerator.MoveNext();
                var i = 0;

                foreach (var deserializedCoord in coords)
                {
                    Assert.IsTrue(moveNext);
                    var array = deserializedCoord as JArray;
                    if (array != null && array.Count > 0 && !(array[0] is JValue))
                    {
                        AssertCoordInternal(array, expectedNesting - 1, (IEnumerable<object>)enumerator.Current);
                    }
                    else if (array != null)
                    {
                        var expectedCoord = (GeographicPosition)enumerator.Current;
                        AssertCoordinate(expectedCoord, array);
                    }

                    moveNext = enumerator.MoveNext();
                    i++;
                }
            }
        }

        private static void AssertCoordinate(GeographicPosition expectedCoord, JArray array)
        {
            Assert.AreEqual(expectedCoord.Latitude, (double) array[1], 1e-6);
            Assert.AreEqual(expectedCoord.Longitude, (double) array[0], 1e-6);
        }
    }
}
