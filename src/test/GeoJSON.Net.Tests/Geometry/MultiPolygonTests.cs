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
            var json = GetExpectedJson();

            var expectMultiPolygon = new MultiPolygon(new List<Polygon>
            {
                new Polygon(new List<LineString>
                {
                    new LineString(new List<IPosition>
                    {
                        new GeographicPosition(52.959676831105995, -2.6797102391514338), 
                        new GeographicPosition(52.9608756693609, -2.6769029474483279), 
                        new GeographicPosition(52.908449372833715, -2.6079763270327119), 
                        new GeographicPosition(52.891287242948195, -2.5815104708998668), 
                        new GeographicPosition(52.875476700983896, -2.5851645010668989), 
                        new GeographicPosition(52.882954723868622, -2.6050779098387191), 
                        new GeographicPosition(52.875255907042678, -2.6373482332006359), 
                        new GeographicPosition(52.878791122091066, -2.6932445076063951), 
                        new GeographicPosition(52.89564268523565, -2.6931334629377890), 
                        new GeographicPosition(52.930592009390175, -2.6548779332193022), 
                        new GeographicPosition(52.959676831105995, -2.6797102391514338)
                    })
                }), 
                new Polygon(new List<LineString>
                {
                    new LineString(new List<IPosition>
                    {
                        new GeographicPosition(52.89610842810761, -2.69628632041613), 
                        new GeographicPosition(52.8894641454077, -2.75901233808515), 
                        new GeographicPosition(52.89938894657412, -2.7663172788742449), 
                        new GeographicPosition(52.90253773227807, -2.804554822840895), 
                        new GeographicPosition(52.929801009654575, -2.83848602260174), 
                        new GeographicPosition(52.94013913205788, -2.838979264607087), 
                        new GeographicPosition(52.937353122653533, -2.7978187468478741), 
                        new GeographicPosition(52.920394929466184, -2.772273870352612), 
                        new GeographicPosition(52.926572918779222, -2.6996509024137052), 
                        new GeographicPosition(52.89610842810761, -2.69628632041613)
                    })
                })
            });

            var actualMultiPolygon = JsonConvert.DeserializeObject<MultiPolygon>(json);

            Assert.AreEqual(expectMultiPolygon, actualMultiPolygon);
        }

        [Test]
        public void Can_Serialize()
        {
            // Arrang
            var polygon1 = new Polygon(new List<LineString>
            {
                new LineString(new List<GeographicPosition>
                {
                    new GeographicPosition(0, 0), 
                    new GeographicPosition(0, 1), 
                    new GeographicPosition(1, 1), 
                    new GeographicPosition(1, 0), 
                    new GeographicPosition(0, 0)
                })
            });

            var polygon2 = new Polygon(new List<LineString>
            {
                new LineString(new List<GeographicPosition>
                {
                    new GeographicPosition(100, 100), 
                    new GeographicPosition(100, 101), 
                    new GeographicPosition(101, 101), 
                    new GeographicPosition(101, 100), 
                    new GeographicPosition(100, 100)
                }), 
                new LineString(new List<GeographicPosition>
                {
                    new GeographicPosition(200, 200), 
                    new GeographicPosition(200, 201), 
                    new GeographicPosition(201, 201), 
                    new GeographicPosition(201, 200), 
                    new GeographicPosition(200, 200)
                })
            });

            var multiPolygon = new MultiPolygon(new List<Polygon> { polygon1, polygon2 });
            var expectedJson = GetExpectedJson();

            // Act
            var actualJson = JsonConvert.SerializeObject(multiPolygon);

            // Assert
            JsonAssert.AreEqual(expectedJson, actualJson);
        }
    }
}