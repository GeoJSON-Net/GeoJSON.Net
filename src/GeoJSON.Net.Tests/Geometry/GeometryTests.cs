using System;
using System.Collections.Generic;
using GeoJSON.Net.Converters;
using GeoJSON.Net.Geometry;
using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Geometry
{
    [TestFixture]
    public class GeometryTests : TestBase
    {
        public static IEnumerable<IGeometryObject> Geometries
        {
            get
            {
                var point = new Point(new Position(1, 2, 3));

                var multiPoint = new MultiPoint(new List<Point>
                {
                    new Point(new Position(52.379790828551016, 5.3173828125)),
                    new Point(new Position(52.36721467920585, 5.456085205078125)),
                    new Point(new Position(52.303440474272755, 5.386047363281249, 4.23))
                });

                var lineString = new LineString(new List<IPosition>
                {
                    new Position(52.379790828551016, 5.3173828125),
                    new Position(52.36721467920585, 5.456085205078125),
                    new Position(52.303440474272755, 5.386047363281249, 4.23)
                });

                var multiLineString = new MultiLineString(new List<LineString>
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

                var polygon = new Polygon(new List<LineString>
                {
                    new LineString(new List<IPosition>
                    {
                        new Position(52.379790828551016, 5.3173828125),
                        new Position(52.36721467920585, 5.456085205078125),
                        new Position(52.303440474272755, 5.386047363281249, 4.23),
                        new Position(52.379790828551016, 5.3173828125)
                    })
                });

                var multiPolygon = new MultiPolygon(new List<Polygon>
                {
                    new Polygon(new List<LineString>
                    {
                        new LineString(new List<IPosition>
                        {
                            new Position(52.959676831105995, -2.6797102391514338),
                            new Position(52.9608756693609, -2.6769029474483279),
                            new Position(52.908449372833715, -2.6079763270327119),
                            new Position(52.891287242948195, -2.5815104708998668),
                            new Position(52.875476700983896, -2.5851645010668989),
                            new Position(52.882954723868622, -2.6050779098387191),
                            new Position(52.875255907042678, -2.6373482332006359),
                            new Position(52.878791122091066, -2.6932445076063951),
                            new Position(52.89564268523565, -2.6931334629377890),
                            new Position(52.930592009390175, -2.6548779332193022),
                            new Position(52.959676831105995, -2.6797102391514338)
                        })
                    }),
                    new Polygon(new List<LineString>
                    {
                        new LineString(new List<IPosition>
                        {
                            new Position(52.89610842810761, -2.69628632041613),
                            new Position(52.8894641454077, -2.75901233808515),
                            new Position(52.89938894657412, -2.7663172788742449),
                            new Position(52.90253773227807, -2.804554822840895),
                            new Position(52.929801009654575, -2.83848602260174),
                            new Position(52.94013913205788, -2.838979264607087),
                            new Position(52.937353122653533, -2.7978187468478741),
                            new Position(52.920394929466184, -2.772273870352612),
                            new Position(52.926572918779222, -2.6996509024137052),
                            new Position(52.89610842810761, -2.69628632041613)
                        })
                    })
                });

                yield return point;
                yield return multiPoint;
                yield return lineString;
                yield return multiLineString;
                yield return polygon;
                yield return multiPolygon;
                yield return new GeometryCollection(new List<IGeometryObject>
                {
                    point,
                    multiPoint,
                    lineString,
                    multiLineString,
                    polygon,
                    multiPolygon
                });
            }
        }

        [Test]
        [TestCaseSource(typeof(GeometryTests), nameof(Geometries))]
        public void Can_Serialize_And_Deserialize_Geometry(IGeometryObject geometry)
        {
            var json = JsonSerializer.Serialize<IGeometryObject>(geometry);

            var deserializedGeometry = JsonSerializer.Deserialize<IGeometryObject>(json);

            Assert.AreEqual(geometry, deserializedGeometry);
        }

        [Test]
        [TestCaseSource(typeof(GeometryTests), nameof(Geometries))]
        public void Serialization_Observes_Indenting_Setting_Of_Serializer(IGeometryObject geometry)
        {
            var options = new JsonSerializerOptions {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(geometry, options);
            Assert.IsTrue(json.Contains(Environment.NewLine));
        }

        [Test]
        [TestCaseSource(typeof(GeometryTests), nameof(Geometries))]
        public void Serialization_Observes_No_Indenting_Setting_Of_Serializer(IGeometryObject geometry)
        {
            var json = JsonSerializer.Serialize(geometry, new JsonSerializerOptions { WriteIndented = false });
            Assert.IsFalse(json.Contains(" "));
        }

        [Test]
        [TestCaseSource(typeof(GeometryTests), nameof(Geometries))]
        public void Can_Serialize_And_Deserialize_Geometry_As_Object_Property(IGeometryObject geometry)
        {
            var classWithGeometry = new ClassWithGeometryProperty(geometry);

            var json = JsonSerializer.Serialize(classWithGeometry);

            var deserializedClassWithGeometry = JsonSerializer.Deserialize<ClassWithGeometryProperty>(json);

            Assert.AreEqual(classWithGeometry, deserializedClassWithGeometry);
        }

        [Test]
        [TestCaseSource(typeof(GeometryTests), nameof(Geometries))]
        public void Serialized_And_Deserialized_Equals_And_Share_HashCode(IGeometryObject geometry)
        {
            var classWithGeometry = new ClassWithGeometryProperty(geometry);

            var json = JsonSerializer.Serialize(classWithGeometry);

            var deserializedClassWithGeometry = JsonSerializer.Deserialize<ClassWithGeometryProperty>(json);

            var actual = classWithGeometry;
            var expected = deserializedClassWithGeometry;
            
            Assert.IsTrue(actual.Equals(expected));
            Assert.IsTrue(actual.Equals(actual));

            Assert.IsTrue(expected.Equals(actual));
            Assert.IsTrue(expected.Equals(expected));

            Assert.IsTrue(classWithGeometry == deserializedClassWithGeometry);
            Assert.IsTrue(deserializedClassWithGeometry == classWithGeometry);

            Assert.AreEqual(actual.GetHashCode(), expected.GetHashCode());
        }

        internal class ClassWithGeometryProperty
        {
            public ClassWithGeometryProperty(IGeometryObject geometry)
            {
                Geometry = geometry;
            }

            [JsonConverter(typeof(GeometryConverter))]
            public IGeometryObject Geometry { get; set; }

            /// <summary>
            ///     Determines whether the specified <see cref="T:System.Object" /> is equal to the current
            ///     <see cref="T:System.Object" />.
            /// </summary>
            /// <returns>
            ///     true if the specified object  is equal to the current object; otherwise, false.
            /// </returns>
            /// <param name="obj">The object to compare with the current object. </param>
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }

                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                if (obj.GetType() != GetType())
                {
                    return false;
                }

                return Equals((ClassWithGeometryProperty)obj);
            }

            /// <summary>
            ///     Serves as a hash function for a particular type.
            /// </summary>
            /// <returns>
            ///     A hash code for the current <see cref="T:System.Object" />.
            /// </returns>
            public override int GetHashCode()
            {
                return Geometry.GetHashCode();
            }

            public static bool operator ==(ClassWithGeometryProperty left, ClassWithGeometryProperty right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(ClassWithGeometryProperty left, ClassWithGeometryProperty right)
            {
                return !Equals(left, right);
            }

            private bool Equals(ClassWithGeometryProperty other)
            {
                return Geometry.Equals(other.Geometry);
            }
        }
    }
}