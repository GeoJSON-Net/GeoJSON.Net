using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Feature
{
    [TestFixture]
    internal class GenericFeatureTests : TestBase
    {
        [Test]
        public void Can_Deserialize_Point_Feature()
        {
            var json = GetExpectedJson();

            var feature = JsonConvert.DeserializeObject<Feature<Point>>(json);

            Assert.That(feature, Is.Not.Null);
            Assert.That(feature.Properties, Is.Not.Null);
            Assert.That(feature.Properties.Any());

            Assert.That(feature.Properties.ContainsKey("name"));
            Assert.That(feature.Properties["name"], Is.EqualTo("Dinagat Islands"));

            Assert.That(feature.Id, Is.EqualTo("test-id"));

            Assert.That(feature.Geometry.Type, Is.EqualTo(GeoJSONObjectType.Point));
            Assert.That(feature.Geometry.Coordinates.Longitude, Is.EqualTo(125.6));
            Assert.That(feature.Geometry.Coordinates.Latitude, Is.EqualTo(10.1));
            Assert.That(feature.Geometry.Coordinates.Altitude, Is.EqualTo(456));
        }

        [Test]
        public void Can_Deserialize_LineString_Feature()
        {
            var json = GetExpectedJson();

            var feature = JsonConvert.DeserializeObject<Feature<LineString>>(json);

            Assert.That(feature, Is.Not.Null);
            Assert.That(feature.Properties, Is.Not.Null);
            Assert.That(feature.Properties.Any());

            Assert.That(feature.Properties.ContainsKey("name"));
            Assert.That(feature.Properties["name"], Is.EqualTo("Dinagat Islands"));

            Assert.That(feature.Id, Is.EqualTo("test-id"));

            Assert.That(feature.Geometry.Type, Is.EqualTo(GeoJSONObjectType.LineString));

            Assert.That(feature.Geometry.Coordinates.Count, Is.EqualTo(4));

            //Assert.AreEqual(125.6, feature.Geometry.Coordinates.Longitude);
            //Assert.AreEqual(10.1, feature.Geometry.Coordinates.Latitude);
            //Assert.AreEqual(456, feature.Geometry.Coordinates.Altitude);
        }

        [Test]
        public void Feature_Generic_Equals_Null_Issure94()
        {
            bool equal1 = true;
            bool equal2 = true;

            var point = new Point(new Position(34, 123));
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

            Assert.That(equal1, Is.False);
            Assert.That(equal2, Is.False);
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

            Assert.That(feature, Is.Not.Null);

            Assert.That(feature.Properties, Is.Not.Null);
            Assert.That(feature.Properties.Name, Is.EqualTo("Dinagat Islands"));
            Assert.That(feature.Properties.Value, Is.EqualTo(4.2));

            Assert.That(feature.Id, Is.EqualTo("test-id"));

            Assert.That(feature.Geometry.Type, Is.EqualTo(GeoJSONObjectType.Point));
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
