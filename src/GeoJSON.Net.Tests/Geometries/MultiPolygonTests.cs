using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Geometry
{
    [TestFixture]
    public class MultiPolygonTests : TestBase
    {
        [Test]
        public void Can_Deserialize()
        {
            var json = GetExpectedJson("Can_Deserialize");

            var expectMultiPolygon = GetMultiPolygon();

            var actualMultiPolygon = JsonConvert.DeserializeObject<MultiPolygon>(json);

            Assert.AreEqual(expectMultiPolygon, actualMultiPolygon);
        }

        private MultiPolygon GetMultiPolygon(double offset = 0.0)
        {
            var multiPolygon = new MultiPolygon(new List<Polygon>
            {
                new Polygon(new List<LineString>
                {
                    new LineString(new List<IPosition>
                    {
                        new Position(52.959676831105995 + offset, -2.6797102391514338 + offset),
                        new Position(52.9608756693609 + offset, -2.6769029474483279 + offset),
                        new Position(52.908449372833715 + offset, -2.6079763270327119 + offset),
                        new Position(52.891287242948195 + offset, -2.5815104708998668 + offset),
                        new Position(52.875476700983896 + offset, -2.5851645010668989 + offset),
                        new Position(52.882954723868622 + offset, -2.6050779098387191 + offset),
                        new Position(52.875255907042678 + offset, -2.6373482332006359 + offset),
                        new Position(52.878791122091066 + offset, -2.6932445076063951 + offset),
                        new Position(52.89564268523565 + offset, -2.6931334629377890 + offset),
                        new Position(52.930592009390175 + offset, -2.6548779332193022 + offset),
                        new Position(52.959676831105995 + offset, -2.6797102391514338 + offset)
                    })
                }),
                new Polygon(new List<LineString>
                {
                    new LineString(new List<IPosition>
                    {
                        new Position(52.89610842810761 + offset,-2.69628632041613 + offset),
                        new Position(52.8894641454077 + offset,-2.75901233808515 + offset),
                        new Position(52.89938894657412 + offset,-2.7663172788742449 + offset),
                        new Position(52.90253773227807 + offset,-2.804554822840895 + offset),
                        new Position(52.929801009654575 + offset,-2.83848602260174 + offset),
                        new Position(52.94013913205788 + offset,-2.838979264607087 + offset),
                        new Position(52.937353122653533 + offset,-2.7978187468478741 + offset),
                        new Position(52.920394929466184 + offset,-2.772273870352612 + offset),
                        new Position(52.926572918779222 + offset,-2.6996509024137052 + offset),
                        new Position(52.89610842810761 + offset, -2.69628632041613 + offset)
                    })
                })
            });
            return multiPolygon;
        }

        [Test]
        public void Can_Serialize()
        {
            // Arrang
            var polygon1 = new Polygon(new List<LineString>
            {
                new LineString(new List<IPosition>
                {
                    new Position(0, 0), 
                    new Position(0, 1), 
                    new Position(1, 1), 
                    new Position(1, 0), 
                    new Position(0, 0)
                })
            });

            var polygon2 = new Polygon(new List<LineString>
            {
                new LineString(new List<IPosition>
                {
                    new Position(100, 100), 
                    new Position(100, 101), 
                    new Position(101, 101), 
                    new Position(101, 100), 
                    new Position(100, 100)
                }), 
                new LineString(new List<IPosition>
                {
                    new Position(200, 200), 
                    new Position(200, 201), 
                    new Position(201, 201), 
                    new Position(201, 200), 
                    new Position(200, 200)
                })
            });

            var multiPolygon = new MultiPolygon(new List<Polygon> { polygon1, polygon2 });
            var expectedJson = GetExpectedJson("Can_Serialize");

            // Act
            var actualJson = JsonConvert.SerializeObject(multiPolygon);

            // Assert
            JsonAssert.AreEqual(expectedJson, actualJson);
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

            var left = GetMultiPolygon(offset);
            var right = GetMultiPolygon(offset);

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