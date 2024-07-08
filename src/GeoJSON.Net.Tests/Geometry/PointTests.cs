using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;

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

            ClassicAssert.IsNotNull(actualPoint);
            ClassicAssert.IsNotNull(actualPoint.Coordinates);
            ClassicAssert.AreEqual(53.2455662, actualPoint.Coordinates.Latitude);
            ClassicAssert.AreEqual(90.65464646, actualPoint.Coordinates.Longitude);
            ClassicAssert.AreEqual(200.4567, actualPoint.Coordinates.Altitude);
            ClassicAssert.AreEqual(expectedPoint, actualPoint);
        }

        [Test]
        public void Can_Deserialize_With_Lat_Lon()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662],\"type\":\"Point\"}";

            var expectedPoint = new Point(new Position(53.2455662, 90.65464646));

            var actualPoint = JsonConvert.DeserializeObject<Point>(json);

            ClassicAssert.IsNotNull(actualPoint);
            ClassicAssert.IsNotNull(actualPoint.Coordinates);
            ClassicAssert.AreEqual(53.2455662, actualPoint.Coordinates.Latitude);
            ClassicAssert.AreEqual(90.65464646, actualPoint.Coordinates.Longitude);
            ClassicAssert.IsFalse(actualPoint.Coordinates.Altitude.HasValue);
            ClassicAssert.IsNull(actualPoint.Coordinates.Altitude);
            ClassicAssert.AreEqual(expectedPoint, actualPoint);
        }

        [Test]
        public void Equals_GetHashCode_Contract()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662],\"type\":\"Point\"}";

            var expectedPoint = new Point(new Position(53.2455662, 90.65464646));

            var actualPoint = JsonConvert.DeserializeObject<Point>(json);

            ClassicAssert.AreEqual(expectedPoint, actualPoint);
            ClassicAssert.IsTrue(expectedPoint.Equals(actualPoint));
            ClassicAssert.IsTrue(actualPoint.Equals(expectedPoint));

            ClassicAssert.AreEqual(expectedPoint.GetHashCode(), actualPoint.GetHashCode());
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