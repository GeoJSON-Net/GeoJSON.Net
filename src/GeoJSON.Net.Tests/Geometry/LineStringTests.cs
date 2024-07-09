using System;
using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Geometry
{
    [TestFixture]
    public class LineStringTests : TestBase
    {
        [Test]
        public void Is_Closed()
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314, 4.889259338378906),
                new Position(52.3711451105601, 4.895267486572266),
                new Position(52.36931095278263, 4.892091751098633),
                new Position(52.370725881211314, 4.889259338378906)
            };

            var lineString = new LineString(coordinates);

            Assert.That(lineString.IsClosed());
        }

        [Test]
        public void Is_Not_Closed()
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314, 4.889259338378906),
                new Position(52.3711451105601, 4.895267486572266),
                new Position(52.36931095278263, 4.892091751098633),
                new Position(52.370725881211592, 4.889259338378955)
            };

            var lineString = new LineString(coordinates);

            Assert.That(lineString.IsClosed(), Is.False);
        }
        

        [Test]
        public void Can_Serialize()
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314, 4.889259338378906),
                new Position(52.3711451105601, 4.895267486572266),
                new Position(52.36931095278263, 4.892091751098633),
                new Position(52.370725881211314, 4.889259338378906)
            };

            var lineString = new LineString(coordinates);

            var actualJson = JsonConvert.SerializeObject(lineString);

            JsonAssert.AreEqual(GetExpectedJson(), actualJson);
        }

        [Test]
        public void Can_Deserialize()
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314, 4.889259338378906),
                new Position(52.3711451105601, 4.895267486572266),
                new Position(52.36931095278263, 4.892091751098633),
                new Position(52.370725881211314, 4.889259338378906)
            };

            var expectedLineString = new LineString(coordinates);

            var json = GetExpectedJson();
            var actualLineString = JsonConvert.DeserializeObject<LineString>(json);

            Assert.That(actualLineString, Is.EqualTo(expectedLineString));

            Assert.That(actualLineString.Coordinates.Count, Is.EqualTo(4));
            Assert.That(actualLineString.Coordinates[0].Latitude, Is.EqualTo(expectedLineString.Coordinates[0].Latitude));
            Assert.That(actualLineString.Coordinates[0].Longitude, Is.EqualTo(expectedLineString.Coordinates[0].Longitude));
        }

        [Test]
        public void Constructor_No_Coordinates_Throws_Exception()
        {
            var coordinates = new List<IPosition>();
            Assert.Throws<ArgumentOutOfRangeException>(() => new LineString(coordinates));
        }

        [Test]
        public void Constructor_Null_Coordinates_Throws_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new LineString((IEnumerable<IPosition>)null));
        }

        private LineString GetLineString(double offset = 0.0)
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314 + offset, 4.889259338378906 + offset),
                new Position(52.3711451105601 + offset, 4.895267486572266 + offset),
                new Position(52.36931095278263 + offset, 4.892091751098633 + offset),
                new Position(52.370725881211314 + offset, 4.889259338378906 + offset)
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

            var left = GetLineString(offset);
            var right = GetLineString(offset);

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