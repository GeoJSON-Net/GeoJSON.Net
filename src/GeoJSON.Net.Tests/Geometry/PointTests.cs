using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Geometry
{
    [TestFixture]
    public class PointTests : TestBase
    {
        [Test]
        public void Can_Serialize_With_Lat_Lon()
        {
            var point = new Point(new GeographicPosition(53.2455662, 90.65464646));

            var expectedJson = "{\"coordinates\":[90.65464646,53.2455662],\"type\":\"Point\"}";

            var actualJson = JsonConvert.SerializeObject(point);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }
        
        [Test]
        public void Can_Serialize_With_Lat_Lon_Alt()
        {
            var point = new Point(new GeographicPosition(53.2455662, 90.65464646, 200.4567));

            var expectedJson = "{\"coordinates\":[90.65464646,53.2455662,200.4567],\"type\":\"Point\"}";

            var actualJson = JsonConvert.SerializeObject(point);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Deserialize_With_Lat_Lon_Alt()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662,200.4567],\"type\":\"Point\"}";

            var expectedPoint = new Point(new GeographicPosition(53.2455662, 90.65464646, 200.4567));

            var actualPoint = JsonConvert.DeserializeObject<Point>(json);

            Assert.AreEqual(expectedPoint, actualPoint);
        }

        [Test]
        public void Can_Deserialize_With_Lat_Lon()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662],\"type\":\"Point\"}";

            var expectedPoint = new Point(new GeographicPosition(53.2455662, 90.65464646));

            var actualPoint = JsonConvert.DeserializeObject<Point>(json);

            Assert.AreEqual(expectedPoint, actualPoint);
        }

        [Test]
        public void Equals_GetHashCode_Contract()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662],\"type\":\"Point\"}";

            var expectedPoint = new Point(new GeographicPosition(53.2455662, 90.65464646));

            var actualPoint = JsonConvert.DeserializeObject<Point>(json);

            Assert.AreEqual(expectedPoint, actualPoint);
            Assert.IsTrue(expectedPoint.Equals(actualPoint));
            Assert.IsTrue(actualPoint.Equals(expectedPoint));

            Assert.AreEqual(expectedPoint.GetHashCode(), actualPoint.GetHashCode());
        }

    }
}