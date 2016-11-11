using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Geometry
{
    [TestFixture]
    public class MultiPointTests : TestBase
    {
        [Test]
        public void Can_Serialize()
        {
            var points = new List<Point>
            {
                new Point(new GeographicPosition(52.370725881211314, 4.889259338378906)),
                new Point(new GeographicPosition(52.3711451105601, 4.895267486572266)),
                new Point(new GeographicPosition(52.36931095278263, 4.892091751098633)),
                new Point(new GeographicPosition(52.370725881211314, 4.889259338378906))
            };

            var multiPoint = new MultiPoint(points);

            var actualJson = JsonConvert.SerializeObject(multiPoint);

            JsonAssert.AreEqual(GetExpectedJson(), actualJson);
        }

        [Test]
        public void Can_Deserialize()
        {
            var points = new List<Point>
            {
                new Point(new GeographicPosition(39.57422, -105.01621)),
                new Point(new GeographicPosition(35.0539943, -80.6665134)),
            };

            var expectedMultiPoint = new MultiPoint(points);

            var json = GetExpectedJson();
            var actualMultiPoint = JsonConvert.DeserializeObject<MultiPoint>(json);

            Assert.AreEqual(expectedMultiPoint, actualMultiPoint);
        }
    }
}