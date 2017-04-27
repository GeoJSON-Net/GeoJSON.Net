using GeoJSON.Net.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GeoJSON.Net.Tests
{
    [TestFixture]
    public class MiscTest
    {
        /// <summary>
        /// Test that the last coordinate must be the same as the first to complete the polygon
        /// </summary>
        [Test]
        public void LineStringIsClosed()
        {
            var coordinates = new List<GeographicPosition> 
            { 
                new GeographicPosition(52.370725881211314, 4.889259338378906), 
                new GeographicPosition(52.3711451105601, 4.895267486572266), 
                new GeographicPosition(52.36931095278263, 4.892091751098633), 
                new GeographicPosition(52.370725881211314, 4.889259338378906) 
            }.ToList<IPosition>();

            var lineString = new LineString(coordinates);
        }


        [Test]
        public void ComparePolygons()
        {
            var coordinates = new List<GeographicPosition> 
                { 
                    new GeographicPosition(52.370725881211314, 4.889259338378906), 
                    new GeographicPosition(52.3711451105601, 4.895267486572266), 
                    new GeographicPosition(52.36931095278263, 4.892091751098633), 
                    new GeographicPosition(52.370725881211314, 4.889259338378906) 
                }.ToList<IPosition>();

            var coordinates2 = new List<GeographicPosition> 
                { 
                    new GeographicPosition(52.370725881211314, 4.889259338378906), 
                    new GeographicPosition(52.3711451105601, 4.895267486572266), 
                    new GeographicPosition(52.36931095278263, 4.892091751098633), 
                    new GeographicPosition(52.370725881211314, 4.889259338378906) 
                }.ToList<IPosition>();

            var polygon = new Polygon(new List<LineString> { new LineString(coordinates) });
            var polygon2 = new Polygon(new List<LineString> { new LineString(coordinates2) });

            Assert.IsTrue(polygon == polygon2);

            var coordinates3 = new List<GeographicPosition> 
                { 
                    new GeographicPosition(52.3707258811314, 4.889259338378906), 
                    new GeographicPosition(52.3711451105601, 4.895267486572266), 
                    new GeographicPosition(52.362095278263, 4.892091751098633), 
                    new GeographicPosition(52.3707258811314, 4.889259338378906) 
                }.ToList<IPosition>();
            var polygon3 = new Polygon(new List<LineString> { new LineString(coordinates3) });
            Assert.IsFalse(polygon == polygon3);


            polygon.Coordinates[0].Coordinates.Add(new GeographicPosition(52.370725881211314, 4.889259338378906));
            Assert.IsFalse(polygon == polygon2);

        }

        [Test]
        public void TestFeatureFromClass()
        {
            var testObject = new MyTestClass()
                {
                    BooleanProperty = true,
                    DateTimeProperty = DateTime.Now,
                    DoubleProperty = 1.2345d,
                    EnumProperty = MyTestEnum.Value1,
                    IntProperty = -1,
                    StringProperty = "Hello, GeoJSON !"
                };

            Feature.Feature feature = new Feature.Feature(new Point(new GeographicPosition(10, 10)), testObject);

            Assert.IsNotNull(feature.Properties);
            Assert.IsTrue(feature.Properties.Count > 1);
            Assert.AreEqual(feature.Properties.Count, 6);

        }

        public enum MyTestEnum
        {
            Undefined,
            Value1,
            Value2
        }
        public class MyTestClass
        {
            public int IntProperty { get; set; }
            public string StringProperty { get; set; }
            public DateTime DateTimeProperty { get; set; }
            public double DoubleProperty { get; set; }
            public bool BooleanProperty { get; set; }
            public MyTestEnum EnumProperty { get; set; }
        }
    }
}