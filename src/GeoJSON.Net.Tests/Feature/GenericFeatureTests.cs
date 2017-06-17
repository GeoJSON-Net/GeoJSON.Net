using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Feature
{

    [TestFixture]
    public class GenericFeatureTests : TestBase
    {
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