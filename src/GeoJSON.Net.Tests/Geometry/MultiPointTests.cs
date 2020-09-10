using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using System.Text.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Geometry
{
    [TestFixture]
    public class MultiPointTests : TestBase
    {
        [Test]
        public void Can_Serialize()
        {
            var positions = new List<Position>
            {
                new Position(52.370725881211314, 4.889259338378906),
                new Position(52.3711451105601, 4.895267486572266),
                new Position(52.36931095278263, 4.892091751098633),
                new Position(52.370725881211314, 4.889259338378906)
            };

            var multiPoint = new MultiPoint(positions);

            var actualJson = JsonSerializer.Serialize(multiPoint, DefaultSerializerOptions);

            JsonAssert.AreEqual(GetExpectedJson(), actualJson);
        }

        [Test]
        public void Can_Deserialize()
        {
            var positions = new List<IPosition>
            {
                new Position(39.57422, -105.01621),
                new Position(35.0539943, -80.6665134),
            };

            var expectedMultiPoint = new MultiPoint(positions);

            var json = GetExpectedJson();
            var actualMultiPoint = JsonSerializer.Deserialize<MultiPoint>(json, DefaultSerializerOptions);

            Assert.AreEqual(expectedMultiPoint, actualMultiPoint);
        }

        private List<IPosition> GetPositions(double offset)
        {
            var points = new List<IPosition>
            {
                new Position(52.370725881211314 + offset, 4.889259338378906 + offset),
                new Position(52.3711451105601 + offset, 4.895267486572266 + offset),
                new Position(52.36931095278263 + offset, 4.892091751098633 + offset),
                new Position(52.370725881211314 + offset, 4.889259338378906 + offset)
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

            var left = new MultiPoint(GetPositions(offset));
            var right = new MultiPoint(GetPositions(offset));

            Assert.AreEqual(left, right);
            Assert.AreEqual(right, left);

            Assert.IsTrue(left.Equals(right));
            Assert.IsTrue(left.Equals(left));
            Assert.IsTrue(right.Equals(left));
            Assert.IsTrue(right.Equals(right));

            Assert.IsTrue(left == right);
            Assert.IsTrue(right == left);

            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }
    }
}
