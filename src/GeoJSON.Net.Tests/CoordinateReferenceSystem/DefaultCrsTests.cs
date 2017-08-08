using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Feature;
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

            Assert.IsNull(point.CRS);
        }

        [Test]
        public void Can_Deserialize_CRS_issue_89()
        {
            var json = "{\"coordinates\": [ 90.65464646, 53.2455662, 200.4567 ], \"type\": \"Point\", \"crs\": { \"type\": \"name\", \"properties\": { \"name\": \"urn:ogc:def:crs:OGC:1.3:CRS84\" }}}";

            var point = JsonConvert.DeserializeObject<Point>(json);

            Assert.IsNotNull(point.CRS);
            Assert.AreEqual(CRSType.Name, point.CRS.Type);
        }

        [Test]
        public void Can_Serialize_CRS_issue_89()
        {
            var expected =
                "{\"type\":\"Point\",\"coordinates\":[34.56,12.34],\"crs\":{\"properties\":{\"name\":\"TEST NAME\"},\"type\":\"name\"}}";
            var point = new Point(new Position(12.34, 34.56)) { CRS = new NamedCRS("TEST NAME") };

            var json = JsonConvert.SerializeObject(point);

            Assert.IsNotNull(json);
            Assert.AreEqual(expected, json);
        }

        [Test]
        public void Can_Serialize_DefaultCRS_issue_89()
        {
            var expected =
                "{\"type\":\"Point\",\"coordinates\":[34.56,12.34],\"crs\":{\"properties\":{\"name\":\"urn:ogc:def:crs:OGC::CRS84\"},\"type\":\"name\"}}";
            var point = new Point(new Position(12.34, 34.56)) { CRS = new NamedCRS("urn:ogc:def:crs:OGC::CRS84") };

            var json = JsonConvert.SerializeObject(point);

            Assert.IsNotNull(json);
            Assert.AreEqual(expected, json);
        }
    }
}