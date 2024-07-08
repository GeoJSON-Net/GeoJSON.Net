using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;

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

            ClassicAssert.IsNotNull(feature);
            ClassicAssert.IsNotNull(feature.Properties);
            ClassicAssert.IsTrue(feature.Properties.Any());

            ClassicAssert.IsTrue(feature.Properties.ContainsKey("name"));
            ClassicAssert.AreEqual("Dinagat Islands", feature.Properties["name"]);

            ClassicAssert.AreEqual("test-id", feature.Id);

            ClassicAssert.AreEqual(GeoJSONObjectType.Point, feature.Geometry.Type);
            ClassicAssert.AreEqual(125.6, feature.Geometry.Coordinates.Longitude);
            ClassicAssert.AreEqual(10.1, feature.Geometry.Coordinates.Latitude);
            ClassicAssert.AreEqual(456, feature.Geometry.Coordinates.Altitude);
        }

        [Test]
        public void Can_Deserialize_LineString_Feature()
        {
            var json = GetExpectedJson();

            var feature = JsonConvert.DeserializeObject<Feature<LineString>>(json);

            ClassicAssert.IsNotNull(feature);
            ClassicAssert.IsNotNull(feature.Properties);
            ClassicAssert.IsTrue(feature.Properties.Any());

            ClassicAssert.IsTrue(feature.Properties.ContainsKey("name"));
            ClassicAssert.AreEqual("Dinagat Islands", feature.Properties["name"]);

            ClassicAssert.AreEqual("test-id", feature.Id);

            ClassicAssert.AreEqual(GeoJSONObjectType.LineString, feature.Geometry.Type);

            ClassicAssert.AreEqual(4, feature.Geometry.Coordinates.Count);

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

            ClassicAssert.DoesNotThrow(() =>
            {
                equal1 = feature == null;
                equal2 = feature.Equals(null);
            });

            ClassicAssert.IsFalse(equal1);
            ClassicAssert.IsFalse(equal2);
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

            ClassicAssert.IsNotNull(feature);

            ClassicAssert.IsNotNull(feature.Properties);
            ClassicAssert.AreEqual(feature.Properties.Name, "Dinagat Islands");
            ClassicAssert.AreEqual(feature.Properties.Value, 4.2);

            ClassicAssert.AreEqual(feature.Id, "test-id");

            ClassicAssert.AreEqual(feature.Geometry.Type, GeoJSONObjectType.Point);
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
