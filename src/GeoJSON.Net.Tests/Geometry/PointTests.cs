using GeoJSON.Net.Geometry;
using System.Text.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Geometry {
    [TestFixture]
    public class PointTests : TestBase
    {
        [Test]
        public void Can_Serialize_With_Lat_Lon()
        {
            var point = new Point(new Position(53.2455662, 90.65464646));

            var expectedJson = "{\"coordinates\":[90.65464646,53.2455662],\"type\":\"Point\"}";

            var actualJson = JsonSerializer.Serialize(point, DefaultSerializerOptions);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Serialize_With_Lat_Lon_Alt()
        {
            var point = new Point(new Position(53.2455662, 90.65464646, 200.4567));

            var expectedJson = "{\"coordinates\":[90.65464646,53.2455662,200.4567],\"type\":\"Point\"}";

            var actualJson = JsonSerializer.Serialize(point, DefaultSerializerOptions);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Deserialize_With_Lat_Lon_Alt()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662,200.4567],\"type\":\"Point\"}";

            var expectedPoint = new Point(new Position(53.2455662, 90.65464646, 200.4567));

            var actualPoint = JsonSerializer.Deserialize<Point>(json, DefaultSerializerOptions);

            Assert.IsNotNull(actualPoint);
            Assert.IsNotNull(actualPoint.Coordinates);
            Assert.AreEqual(53.2455662, actualPoint.Coordinates.Latitude);
            Assert.AreEqual(90.65464646, actualPoint.Coordinates.Longitude);
            Assert.AreEqual(200.4567, actualPoint.Coordinates.Altitude);
            Assert.AreEqual(expectedPoint, actualPoint);
        }

        [Test]
        public void Can_Deserialize_With_Lat_Lon()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662],\"type\":\"Point\"}";

            var expectedPoint = new Point(new Position(53.2455662, 90.65464646));

            var actualPoint = JsonSerializer.Deserialize<Point>(json, DefaultSerializerOptions);

            Assert.IsNotNull(actualPoint);
            Assert.IsNotNull(actualPoint.Coordinates);
            Assert.AreEqual(53.2455662, actualPoint.Coordinates.Latitude);
            Assert.AreEqual(90.65464646, actualPoint.Coordinates.Longitude);
            Assert.IsFalse(actualPoint.Coordinates.Altitude.HasValue);
            Assert.IsNull(actualPoint.Coordinates.Altitude);
            Assert.AreEqual(expectedPoint, actualPoint);
        }

        [Test]
        public void Equals_GetHashCode_Contract()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662],\"type\":\"Point\"}";

            var expectedPoint = new Point(new Position(53.2455662, 90.65464646));

            var actualPoint = JsonSerializer.Deserialize<Point>(json, DefaultSerializerOptions);

            Assert.AreEqual(expectedPoint, actualPoint);
            Assert.IsTrue(expectedPoint.Equals(actualPoint));
            Assert.IsTrue(actualPoint.Equals(expectedPoint));

            Assert.AreEqual(expectedPoint.GetHashCode(), actualPoint.GetHashCode());
        }

        [Test]
        public void Can_Serialize_With_Lat_Lon_Alt_DefaultValueHandling_Ignore()
        {
            var point = new Point(new Position(53.2455662, 90.65464646, 200.4567));

            var expectedJson = "{\"coordinates\":[90.65464646,53.2455662,200.4567],\"type\":\"Point\"}";

            var actualJson = JsonSerializer.Serialize(point, DefaultSerializerOptions);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }
    }
}
