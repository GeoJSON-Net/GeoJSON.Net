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
            var point = new Point(new Position(53.2455662, 90.65464646));
            
            var expectedJson = "{\"coordinates\":[90.65464646,53.2455662],\"type\":\"Point\"}";

            var actualJson = JsonConvert.SerializeObject(point);
            
            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Serialize_With_Lat_Lon_Alt()
        {
            var point = new Point(new Position(53.2455662, 90.65464646, 200.4567));

            var expectedJson = "{\"coordinates\":[90.65464646,53.2455662,200.4567],\"type\":\"Point\"}";

            var actualJson = JsonConvert.SerializeObject(point);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Deserialize_With_Lat_Lon_Alt()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662,200.4567],\"type\":\"Point\"}";

            var expectedPoint = new Point(new Position(53.2455662, 90.65464646, 200.4567));

            var actualPoint = JsonConvert.DeserializeObject<Point>(json);

            Assert.That(actualPoint, Is.Not.Null);
            Assert.That(actualPoint.Coordinates, Is.Not.Null);
            Assert.That(actualPoint.Coordinates.Latitude, Is.EqualTo(53.2455662));
            Assert.That(actualPoint.Coordinates.Longitude, Is.EqualTo(90.65464646));
            Assert.That(actualPoint.Coordinates.Altitude, Is.EqualTo(200.4567));
            Assert.That(actualPoint, Is.EqualTo(expectedPoint));
        }

        [Test]
        public void Can_Deserialize_With_Lat_Lon()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662],\"type\":\"Point\"}";

            var expectedPoint = new Point(new Position(53.2455662, 90.65464646));

            var actualPoint = JsonConvert.DeserializeObject<Point>(json);

            Assert.That(actualPoint, Is.Not.Null);
            Assert.That(actualPoint.Coordinates, Is.Not.Null);
            Assert.That(actualPoint.Coordinates.Latitude, Is.EqualTo(53.2455662));
            Assert.That(actualPoint.Coordinates.Longitude, Is.EqualTo(90.65464646));
            Assert.That(actualPoint.Coordinates.Altitude.HasValue, Is.False);
            Assert.That(actualPoint.Coordinates.Altitude, Is.Null);
            Assert.That(actualPoint, Is.EqualTo(expectedPoint));
        }

        [Test]
        public void Equals_GetHashCode_Contract()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662],\"type\":\"Point\"}";

            var expectedPoint = new Point(new Position(53.2455662, 90.65464646));

            var actualPoint = JsonConvert.DeserializeObject<Point>(json);

            Assert.That(actualPoint, Is.EqualTo(expectedPoint));
            Assert.That(expectedPoint.Equals(actualPoint));
            Assert.That(actualPoint.Equals(expectedPoint));

            Assert.That(actualPoint.GetHashCode(), Is.EqualTo(expectedPoint.GetHashCode()));
        }

        [Test]
        public void Can_Serialize_With_Lat_Lon_Alt_DefaultValueHandling_Ignore()
        {
            var point = new Point(new Position(53.2455662, 90.65464646, 200.4567));

            var expectedJson = "{\"coordinates\":[90.65464646,53.2455662,200.4567],\"type\":\"Point\"}";

            var actualJson = JsonConvert.SerializeObject(point, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

            JsonAssert.AreEqual(expectedJson, actualJson);
        }
    }
}