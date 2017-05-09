using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Geometries
{
    [TestFixture]
    public class MultiLineStringTests : TestBase
    {
        [Test]
        public void Can_Deserialize()
        {
            var json = GetExpectedJson("Can_Deserialize");

            var expectedMultiLineString = new MultiLineString(new List<LineString>
            {
                new LineString(new List<IPosition>
                {
                    new Position(52.379790828551016, 5.3173828125),
                    new Position(52.36721467920585, 5.456085205078125),
                    new Position(52.303440474272755, 5.386047363281249, 4.23)
                }),
                new LineString(new List<IPosition>
                {
                    new Position(52.379790828551016, 5.3273828125),
                    new Position(52.36721467920585, 5.486085205078125),
                    new Position(52.303440474272755, 5.426047363281249, 4.23)
                })
            });

            var multiLineString = JsonConvert.DeserializeObject<MultiLineString>(json);

            Assert.IsNotNull(multiLineString);
            Assert.AreEqual(expectedMultiLineString, multiLineString);
        }

        [Test]
        public void Can_Serialize()
        {
            var expectedMultiLineString = new MultiLineString(new List<LineString>
            {
                new LineString(new List<IPosition>
                {
                    new Position(52.379790828551016, 5.3173828125),
                    new Position(52.36721467920585, 5.456085205078125),
                    new Position(52.303440474272755, 5.386047363281249, 4.23)
                }),
                new LineString(new List<IPosition>
                {
                    new Position(52.379790828551016, 5.3273828125),
                    new Position(52.36721467920585, 5.486085205078125),
                    new Position(52.303440474272755, 5.426047363281249, 4.23)
                })
            });

            var expectedJson = GetExpectedJson("Can_Serialize");
            var actualJson = JsonConvert.SerializeObject(expectedMultiLineString);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        private LineString GetLineString(double offset = 0.0)
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.379790828551016 + offset, 5.3173828125 + offset),
                new Position(52.36721467920585 + offset, 5.456085205078125 + offset),
                new Position(52.303440474272755 + offset, 5.386047363281249 + offset, 4.23 + offset)
            };
            var lineString = new LineString(coordinates);
            return lineString;
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

            var leftLine = new List<LineString>
            {
                GetLineString(offset + 1),
                GetLineString(offset + 2)
            };

            var left = new MultiLineString(leftLine);

            var rightLine = new List<LineString>
            {
                GetLineString(offset + 1),
                GetLineString(offset + 2)
            };

            var right = new MultiLineString(rightLine);

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