using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace GeoJSON.Net.Tests
{
    [TestFixture]
    public class SerializationTest : TestBase
    {
        [Test]
        public void PolygonSerialization()
        {
            var coordinates = new List<Position> 
                { 
                    new Position(52.370725881211314, 4.889259338378906), 
                    new Position(52.3711451105601, 4.895267486572266), 
                    new Position(52.36931095278263, 4.892091751098633), 
                    new Position(52.370725881211314, 4.889259338378906) 
                }.ToList<IPosition>();

            var model = new Polygon(new List<LineString> { new LineString(coordinates) });
            var serializedData = JsonConvert.SerializeObject(model, Formatting.Indented, DefaultJsonSerializerSettings);

            var matches = Regex.Matches(serializedData, @"(?<coordinates>[0-9]+([.,][0-9]+))");

            double lng;
            double.TryParse(matches[0].Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out lng);

            //Double precision can pose a problem 
            Assert.IsTrue(Math.Abs(lng - 4.889259338378906) < 0.0000001);
            Assert.IsTrue(!serializedData.Contains("latitude"));
        }

        [Test]
        public void MultiLineStringSerialization()
        {
            var coordinates = new[]
            {
                new List<IPosition> 
                { 
                    new Position(52.370725881211314, 4.889259338378906), 
                    new Position(52.3711451105601, 4.895267486572266), 
                    new Position(52.36931095278263, 4.892091751098633), 
                    new Position(52.370725881211314, 4.889259338378906) 
                },
                new List<IPosition> 
                { 
                    new Position(52.370725881211314, 4.989259338378906), 
                    new Position(52.3711451105601, 4.995267486572266), 
                    new Position(52.36931095278263, 4.992091751098633), 
                    new Position(52.370725881211314, 4.989259338378906) 
                },
            };

            var model = new MultiLineString(coordinates.Select(ca => new LineString(ca)).ToList());
            var serializedData = JsonConvert.SerializeObject(model, DefaultJsonSerializerSettings);

            var matches = Regex.Matches(serializedData, @"(?<coordinates>[0-9]+([.,][0-9]+))");

            double lng;
            double.TryParse(matches[0].Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out lng);

            //Double precision can pose a problem 
            Assert.IsTrue(Math.Abs(lng - 4.889259338378906) < 0.0000001);
            Assert.IsTrue(!serializedData.Contains("latitude"));
        }

        [Test]
        public void GeographicPositionSerialization()
        {
            var model = new Position(112.12, 10);

            var serialized = JsonConvert.SerializeObject(model, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            var matches = Regex.Matches(serialized, @"(\d+.\d+)");
            Assert.IsTrue(matches.Count == 2);
            double lng;
            
            Assert.IsTrue(double.TryParse(matches[0].Value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out lng));
            Assert.AreEqual(lng, 112.12);
        }

        [Test]
        public void MultiPolygonSerialization()
        {
            var expectedJson = "{\"geometry\":{\"coordinates\":[[[[0.0,0.0],[1.0,0.0],[1.0,1.0],[0.0,1.0],[0.0,0.0]]],[[[100.0,100.0],[101.0,100.0],[101.0,101.0],[100.0,101.0],[100.0,100.0]],[[200.0,200.0],[201.0,200.0],[201.0,201.0],[200.0,201.0],[200.0,200.0]]]],\"type\":\"MultiPolygon\"},\"properties\":{},\"type\":\"Feature\"}";
            var polygon1 = new Polygon(new List<LineString>
            {
                new LineString((new List<Position>
                {
                    new Position(0, 0),
                    new Position(0, 1),
                    new Position(1, 1),
                    new Position(1, 0),
                    new Position(0, 0)
                }).ToList<IPosition>())

            });

            var polygon2 = new Polygon(new List<LineString>
            {
                new LineString((new List<Position>
                {
                    new Position(100, 100),
                    new Position(100, 101),
                    new Position(101, 101),
                    new Position(101, 100),
                    new Position(100, 100)
                }).ToList<IPosition>()),
                new LineString((new List<Position>
                {
                    new Position(200, 200),
                    new Position(200, 201),
                    new Position(201, 201),
                    new Position(201, 200),
                    new Position(200, 200)
                }).ToList<IPosition>())

            });

            var multipolygon = new MultiPolygon(new List<Polygon> { polygon1, polygon2 });
            var newFeature = new Feature.Feature(multipolygon);
            var serializedData = JsonConvert.SerializeObject(newFeature, Formatting.Indented, DefaultJsonSerializerSettings);
            var serializedDataWithouWhiteSpace = Regex.Replace(serializedData, @"(\s|$)+", "");
            Assert.IsTrue(serializedDataWithouWhiteSpace == expectedJson);
        }


    }
}
