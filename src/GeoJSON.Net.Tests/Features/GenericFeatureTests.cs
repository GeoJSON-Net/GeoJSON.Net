﻿using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Features;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Features
{
    [TestFixture]
    internal class GenericFeatureTests : TestBase
    {
        [Test]
        public void Can_Deserialize_Point_Feature()
        {
            var json = GetExpectedJson();

            var feature = JsonConvert.DeserializeObject<Feature<Point>>(json);

            Assert.IsNotNull(feature);
            Assert.IsNotNull(feature.Properties);
            Assert.IsTrue(feature.Properties.Any());

            Assert.IsTrue(feature.Properties.ContainsKey("name"));
            Assert.AreEqual("Dinagat Islands", feature.Properties["name"]);

            Assert.AreEqual("test-id", feature.Id);

            Assert.AreEqual(GeoJSONObjectType.Point, feature.Geometry.Type);
            Assert.AreEqual(125.6, feature.Geometry.Coordinates.Longitude);
            Assert.AreEqual(10.1, feature.Geometry.Coordinates.Latitude);
            Assert.AreEqual(456, feature.Geometry.Coordinates.Altitude);
        }

        [Test]
        public void Can_Deserialize_LineString_Feature()
        {
            var json = GetExpectedJson();

            var feature = JsonConvert.DeserializeObject<Feature<LineString>>(json);

            Assert.IsNotNull(feature);
            Assert.IsNotNull(feature.Properties);
            Assert.IsTrue(feature.Properties.Any());

            Assert.IsTrue(feature.Properties.ContainsKey("name"));
            Assert.AreEqual("Dinagat Islands", feature.Properties["name"]);

            Assert.AreEqual("test-id", feature.Id);

            Assert.AreEqual(GeoJSONObjectType.LineString, feature.Geometry.Type);

            Assert.AreEqual(4, feature.Geometry.Coordinates.Count);

            //Assert.AreEqual(125.6, feature.Geometry.Coordinates.Longitude);
            //Assert.AreEqual(10.1, feature.Geometry.Coordinates.Latitude);
            //Assert.AreEqual(456, feature.Geometry.Coordinates.Altitude);
        }

        [Test]
        public void Feature_Generic_Equals_Null_Issure94()
        {
            bool equal1 = true;
            bool equal2 = true;

            var point = new Point(new Position(123, 34));
            var properties = new Dictionary<string, string>
            {
                {"test1", "test1val"},
                {"test2", "test2val"}
            };

            var feature = new Feature<Point, Dictionary<string, string>>(point, properties, "testid");

            Assert.DoesNotThrow(() =>
            {
                equal1 = feature == null;
                equal2 = feature.Equals(null);
            });

            Assert.IsFalse(equal1);
            Assert.IsFalse(equal2);
        }

        private class TypedFeatureProps
        {
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("value")]
            public double Value { get; set; }
        }

        [Test]
        public void Can_Deserialize_Typed_Point_Feature()
        {
            var json = GetExpectedJson();
            var feature = JsonConvert.DeserializeObject<Feature<Point, TypedFeatureProps>>(json);

            Assert.IsNotNull(feature);

            Assert.IsNotNull(feature.Properties);
            Assert.AreEqual(feature.Properties.Name, "Dinagat Islands");
            Assert.AreEqual(feature.Properties.Value, 4.2);

            Assert.AreEqual(feature.Id, "test-id");

            Assert.AreEqual(feature.Geometry.Type, GeoJSONObjectType.Point);
        }


        [Test]
        public void Can_Serialize_Typed_Point_Feature()
        {
            var geometry = new Point(new Position(1, 2));
            var props = new TypedFeatureProps
            {
                Name = "no name here",
                Value = 1.337
            };
            var feature = new Feature<Point, TypedFeatureProps>(geometry, props, "no id there");

            var expectedJson = GetExpectedJson();
            var actualJson = JsonConvert.SerializeObject(feature);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }
    }
}
