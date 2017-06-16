using System.Linq;
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

            var feature = JsonConvert.DeserializeObject<Net.Feature.Feature<Point>>(json);

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

            var feature = JsonConvert.DeserializeObject<Net.Feature.Feature<LineString>>(json);

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
    }
}
