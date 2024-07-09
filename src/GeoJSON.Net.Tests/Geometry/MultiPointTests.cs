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
                new Point(new Position(52.370725881211314, 4.889259338378906)),
                new Point(new Position(52.3711451105601, 4.895267486572266)),
                new Point(new Position(52.36931095278263, 4.892091751098633)),
                new Point(new Position(52.370725881211314, 4.889259338378906))
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
                new Point(new Position(39.57422, -105.01621)),
                new Point(new Position(35.0539943, -80.6665134)),
            };

            var expectedMultiPoint = new MultiPoint(points);

            var json = GetExpectedJson();
            var actualMultiPoint = JsonConvert.DeserializeObject<MultiPoint>(json);

            Assert.That(actualMultiPoint, Is.EqualTo(expectedMultiPoint));
        }

        private List<Point> GetPoints(double offset)
        {
            var points = new List<Point>
            {
                new Point(new Position(52.370725881211314 + offset, 4.889259338378906 + offset)),
                new Point(new Position(52.3711451105601 + offset, 4.895267486572266 + offset)),
                new Point(new Position(52.36931095278263 + offset, 4.892091751098633 + offset)),
                new Point(new Position(52.370725881211314 + offset, 4.889259338378906 + offset))
            };
            return points;
        }

        [Test]
        public void Equals_GetHashCode_Contract()
        {
            var rnd = new System.Random();
            var offset = rnd.NextDouble() * 60;
            if (rnd.NextDouble() < 0.5)
            {
                offset *= -1;
            }

            var left = new MultiPoint(GetPoints(offset));
            var right = new MultiPoint(GetPoints(offset));

            Assert.That(right, Is.EqualTo(left));
            Assert.That(left, Is.EqualTo(right));

            Assert.That(left.Equals(right));
            Assert.That(left.Equals(left));
            Assert.That(right.Equals(left));
            Assert.That(right.Equals(right));

            Assert.That(left == right);
            Assert.That(right == left);

            Assert.That(right.GetHashCode(), Is.EqualTo(left.GetHashCode()));
        }
    }
}