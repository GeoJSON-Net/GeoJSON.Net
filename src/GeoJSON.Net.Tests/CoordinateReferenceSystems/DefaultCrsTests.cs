using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Features;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.CoordinateReferenceSystem
{
    [TestFixture]
    public class DefaultCrsTests : TestBase
    {
        [Test]
        public void Can_Serialize_Does_Not_Output_Crs_Property()
        {
            var collection = new FeatureCollection();

            var json = JsonConvert.SerializeObject(collection);

            Assert.IsTrue(!json.Contains("\"crs\""));
        }

        [Test]
        public void Can_Deserialize_When_Json_Does_Not_Contain_Crs_Property()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662,200.4567],\"type\":\"Point\"}";

            var point = JsonConvert.DeserializeObject<Point>(json);

            Assert.IsInstanceOf<DefaultCRS>(point.CRS);
        }

        [Test]
        public void Equals_GetHashCode_Contract()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662,200.4567],\"type\":\"Point\"}";

            var point = JsonConvert.DeserializeObject<Point>(json);

            var expected = DefaultCRS.Instance;
            var actual = point.CRS;

            Assert.AreEqual(expected, actual);

            Assert.IsTrue(expected.Equals(actual));
            Assert.IsTrue(actual.Equals(expected));

            Assert.IsTrue(actual.Equals(actual));
            Assert.IsTrue(expected.Equals(expected));

            Assert.IsTrue(expected == actual);
            Assert.IsTrue(actual == expected);

            Assert.AreEqual(expected.GetHashCode(), actual.GetHashCode());

        }
    }
}