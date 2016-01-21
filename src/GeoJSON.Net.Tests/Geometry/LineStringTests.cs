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
            var coordinates = new List<GeographicPosition>
            {
                new GeographicPosition(52.370725881211314, 4.889259338378906),
                new GeographicPosition(52.3711451105601, 4.895267486572266),
                new GeographicPosition(52.36931095278263, 4.892091751098633),
                new GeographicPosition(52.370725881211314, 4.889259338378906)
            };

            var lineString = new LineString(coordinates);

            Assert.IsTrue(lineString.IsClosed());
        }

        [Test]
        public void Is_Closed_FirstTwo()
        {
            var coordinates = new List<GeographicPosition>
            {
                new GeographicPosition(52.370725881211314, 4.889259338378906),
                new GeographicPosition(52.370725881211314, 4.889259338378906),
                new GeographicPosition(52.3711451105601, 4.895267486572266),
                new GeographicPosition(52.36931095278263, 4.892091751098633)
            };

            var lineString = new LineString(coordinates);

            Assert.IsTrue(lineString.IsClosed());
        }

        [Test]
        public void Is_Not_Closed()
        {
            var coordinates = new List<GeographicPosition>
            {
                new GeographicPosition(52.370725881211314, 4.889259338378906),
                new GeographicPosition(52.3711451105601, 4.895267486572266),
                new GeographicPosition(52.36931095278263, 4.892091751098633),
                new GeographicPosition(52.370725881211592, 4.889259338378955)
            };

            var lineString = new LineString(coordinates);

            Assert.IsFalse(lineString.IsClosed());
        }

        [Test]
        public void Can_Serialize()
        {
            var coordinates = new List<GeographicPosition>
            {
                new GeographicPosition(52.370725881211314, 4.889259338378906),
                new GeographicPosition(52.3711451105601, 4.895267486572266),
                new GeographicPosition(52.36931095278263, 4.892091751098633),
                new GeographicPosition(52.370725881211314, 4.889259338378906)
            };

            var lineString = new LineString(coordinates);

            var actualJson = JsonConvert.SerializeObject(lineString);

            JsonAssert.AreEqual(GetExpectedJson(), actualJson);
        }

        [Test]
        public void Can_Deserialize()
        {
            var coordinates = new List<GeographicPosition>
            {
                new GeographicPosition(52.370725881211314, 4.889259338378906),
                new GeographicPosition(52.3711451105601, 4.895267486572266),
                new GeographicPosition(52.36931095278263, 4.892091751098633),
                new GeographicPosition(52.370725881211314, 4.889259338378906)
            };

            var expectedLineString = new LineString(coordinates);

            var json = GetExpectedJson();
            var actualLineString = JsonConvert.DeserializeObject<LineString>(json);

            Assert.AreEqual(expectedLineString, actualLineString);
        }
    }
}