using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoJSON.Net.Tests.Feature
{
    [TestFixture]
    public class FeatureTests : TestBase
    {
        [Test]
        public void Can_Deserialize_Point_Feature()
        {
            var json = GetExpectedJson();

            var feature = JsonConvert.DeserializeObject<Net.Feature.Feature>(json);

            Assert.IsNotNull(feature);
            Assert.IsNotNull(feature.Properties);
            Assert.IsTrue(feature.Properties.Any());

            Assert.IsTrue(feature.Properties.ContainsKey("name"));
            Assert.AreEqual(feature.Properties["name"], "Dinagat Islands");

            Assert.AreEqual("test-id", feature.Id);

            Assert.AreEqual(GeoJSONObjectType.Point, feature.Geometry.Type);
        }

        [Test]
        public void Can_Serialize_LineString_Feature()
        {
            var coordinates = new[]
            {
                new List<IPosition>
                {
                    new Position(52.370725881211314, 4.889259338378906),
                    new Position(52.3711451105601, 4.895267486572266),
                    new Position(52.36931095278263, 4.892091751098633),
                    new Position(52.370725881211314, 4.889259338378906)
                },
                new List<IPosition>
                {
                    new Position(52.370725881211314, 4.989259338378906),
                    new Position(52.3711451105601, 4.995267486572266),
                    new Position(52.36931095278263, 4.992091751098633),
                    new Position(52.370725881211314, 4.989259338378906)
                }
            };

            var geometry = new LineString(coordinates[0]);


            var actualJson = JsonConvert.SerializeObject(new Net.Feature.Feature(geometry));

            Console.WriteLine(actualJson);

            var expectedJson = GetExpectedJson();

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Serialize_MultiLineString_Feature()
        {
            var geometry = new MultiLineString(new List<LineString>
            {
                new LineString(new List<IPosition>
                {
                    new Position(52.370725881211314, 4.889259338378906),
                    new Position(52.3711451105601, 4.895267486572266),
                    new Position(52.36931095278263, 4.892091751098633),
                    new Position(52.370725881211314, 4.889259338378906)
                }),
                new LineString(new List<IPosition>
                {
                    new Position(52.370725881211314, 4.989259338378906),
                    new Position(52.3711451105601, 4.995267486572266),
                    new Position(52.36931095278263, 4.992091751098633),
                    new Position(52.370725881211314, 4.989259338378906)
                })
            });

            var expectedJson = GetExpectedJson();

            var actualJson = JsonConvert.SerializeObject(new Net.Feature.Feature(geometry));

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Serialize_Point_Feature()
        {
            var geometry = new Point(new Position(1, 2));
            var expectedJson = GetExpectedJson();

            var actualJson = JsonConvert.SerializeObject(new Net.Feature.Feature(geometry));

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Serialize_Polygon_Feature()
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314, 4.889259338378906),
                new Position(52.3711451105601, 4.895267486572266),
                new Position(52.36931095278263, 4.892091751098633),
                new Position(52.370725881211314, 4.889259338378906)
            };

            var polygon = new Polygon(new List<LineString> { new LineString(coordinates) });
            var properties = new Dictionary<string, object> { { "Name", "Foo" } };
            var feature = new Net.Feature.Feature(polygon, properties);

            var expectedJson = GetExpectedJson();
            var actualJson = JsonConvert.SerializeObject(feature);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Serialize_MultiPolygon_Feature()
        {
            var multiPolygon = new MultiPolygon(new List<Polygon>
            {
                new Polygon(new List<LineString>
                {
                    new LineString(new List<IPosition>
                    {
                        new Position(0, 0),
                        new Position(0, 1),
                        new Position(1, 1),
                        new Position(1, 0),
                        new Position(0, 0)
                    })
                }),
                new Polygon(new List<LineString>
                {
                    new LineString(new List<IPosition>
                    {
                        new Position(70, 70),
                        new Position(70, 71),
                        new Position(71, 71),
                        new Position(71, 70),
                        new Position(70, 70)
                    }),
                    new LineString(new List<IPosition>
                    {
                        new Position(80, 80),
                        new Position(80, 81),
                        new Position(81, 81),
                        new Position(81, 80),
                        new Position(80, 80)
                    })
                })
            });

            var feature = new Net.Feature.Feature(multiPolygon);

            var expectedJson = GetExpectedJson();
            var actualJson = JsonConvert.SerializeObject(feature);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Ctor_Can_Add_Properties_Using_Object()
        {
            var properties = new TestFeatureProperty
            {
                BooleanProperty = true,
                DateTimeProperty = DateTime.Now,
                DoubleProperty = 1.2345d,
                EnumProperty = TestFeatureEnum.Value1,
                IntProperty = -1,
                StringProperty = "Hello, GeoJSON !"
            };

            Net.Feature.Feature feature = new Net.Feature.Feature(new Point(new Position(10, 10)), properties);

            Assert.IsNotNull(feature.Properties);
            Assert.IsTrue(feature.Properties.Count > 1);
            Assert.AreEqual(feature.Properties.Count, 6);
        }

        [Test]
        public void Ctor_Creates_Properties_Collection_When_Passed_Null_Proper_Object()
        {
            Net.Feature.Feature feature = new Net.Feature.Feature(new Point(new Position(10, 10)), (object)null);

            Assert.IsNotNull(feature.Properties);
            CollectionAssert.IsEmpty(feature.Properties);
        }

        [Test]
        public void Feature_Equals_GetHashCode_Contract_Properties_Of_Objects()
        {
            // order of keys should not matter

            var leftProp = new TestFeatureProperty
            {
                StringProperty = "Hello, GeoJSON !",
                EnumProperty = TestFeatureEnum.Value1,
                IntProperty = -1,
                BooleanProperty = true,
                DateTimeProperty = DateTime.Now,
                DoubleProperty = 1.2345d
            };

            var left = new Net.Feature.Feature(new Point(new Position(10, 10)), leftProp);

            var rightProp = new TestFeatureProperty
            {
                BooleanProperty = true,
                DateTimeProperty = DateTime.Now,
                DoubleProperty = 1.2345d,
                EnumProperty = TestFeatureEnum.Value1,
                IntProperty = -1,
                StringProperty = "Hello, GeoJSON !"
            };

            var right = new Net.Feature.Feature(new Point(new Position(10, 10)), rightProp);

            Assert_Are_Equal(left, right);
        }

        [Test]
        public void Feature_Equals_GetHashCode_Contract_Dictionary()
        {
            var leftDictionary = GetPropertiesInRandomOrder();
            var rightDictionary = GetPropertiesInRandomOrder();

            var geometry10 = new Position(10, 10);
            var geometry20 = new Position(20, 20);

            var left = new Net.Feature.Feature(new Point(
                geometry10),
                leftDictionary,
                "abc");
            var right = new Net.Feature.Feature(new Point(
                geometry20),
                rightDictionary,
                "abc");

            Assert_Are_Not_Equal(left, right); // different geometries


            left = new Net.Feature.Feature(new Point(
                geometry10),
                leftDictionary,
                "abc");
            right = new Net.Feature.Feature(new Point(
                geometry10),
                rightDictionary,
                "abc"); // identical geometries, different ids and or properties or not compared

            Assert_Are_Equal(left, right);

        }

        [Test]
        public void Serialized_And_Deserialized_Feature_Equals_And_Share_HashCode()
        {
            var geometry = GetGeometry();

            var leftFeature = new Net.Feature.Feature(geometry);
            var leftJson = JsonConvert.SerializeObject(leftFeature);
            var left = JsonConvert.DeserializeObject<Net.Feature.Feature>(leftJson);

            var rightFeature = new Net.Feature.Feature(geometry);
            var rightJson = JsonConvert.SerializeObject(rightFeature);
            var right = JsonConvert.DeserializeObject<Net.Feature.Feature>(rightJson);

            Assert_Are_Equal(left, right);

            leftFeature = new Net.Feature.Feature(geometry, GetPropertiesInRandomOrder());
            leftJson = JsonConvert.SerializeObject(leftFeature);
            left = JsonConvert.DeserializeObject<Net.Feature.Feature>(leftJson);

            rightFeature = new Net.Feature.Feature(geometry, GetPropertiesInRandomOrder());
            rightJson = JsonConvert.SerializeObject(rightFeature);
            right = JsonConvert.DeserializeObject<Net.Feature.Feature>(rightJson);

            Assert_Are_Equal(left, right); // assert properties doesn't influence comparison and hashcode

            leftFeature = new Net.Feature.Feature(geometry, null, "abc_abc");
            leftJson = JsonConvert.SerializeObject(leftFeature);
            left = JsonConvert.DeserializeObject<Net.Feature.Feature>(leftJson);

            rightFeature = new Net.Feature.Feature(geometry, null, "xyz_XYZ");
            rightJson = JsonConvert.SerializeObject(rightFeature);
            right = JsonConvert.DeserializeObject<Net.Feature.Feature>(rightJson);

            Assert_Are_Equal(left, right); // assert id's doesn't influence comparison and hashcode

            leftFeature = new Net.Feature.Feature(geometry, GetPropertiesInRandomOrder(), "abc");
            leftJson = JsonConvert.SerializeObject(leftFeature);
            left = JsonConvert.DeserializeObject<Net.Feature.Feature>(leftJson);

            rightFeature = new Net.Feature.Feature(geometry, GetPropertiesInRandomOrder(), "abc");
            rightJson = JsonConvert.SerializeObject(rightFeature);
            right = JsonConvert.DeserializeObject<Net.Feature.Feature>(rightJson);

            Assert_Are_Equal(left, right); // assert id's + properties doesn't influence comparison and hashcode
        }

        [Test]
        public void Feature_Equals_Null_Issue94()
        {
            bool equal1 = true;
            bool equal2 = true;

            var feature = new Net.Feature.Feature(new Point(new Position(12, 123)));
            Assert.DoesNotThrow(() =>
            {
                equal1 = feature.Equals(null);
                equal2 = feature == null;
            });

            Assert.IsFalse(equal1);
            Assert.IsFalse(equal2);
        }

        [Test]
        public void Feature_Null_Instance_Equals_Null_Issue94()
        {
            var equal1 = true;

            Net.Feature.Feature feature = null;
            Assert.DoesNotThrow(() =>
            {
                equal1 = feature != null;
            });

            Assert.IsFalse(equal1);
        }

        [Test]
        public void Feature_Equals_Itself_Issue94()
        {
            bool equal1 = false;
            bool equal2 = false;

            var feature = new Net.Feature.Feature(new Point(new Position(12, 123)));
            Assert.DoesNotThrow(() =>
            {
                equal1 = feature == feature;
                equal2 = feature.Equals(feature);
            });

            Assert.IsTrue(equal1);
            Assert.IsTrue(equal2);
        }

        [Test]
        public void Feature_Equals_Geometry_Null_Issue115()
        {
            bool equal1 = false;
            bool equal2 = false;

            var feature1 = new Net.Feature.Feature(null);
            var feature2 = new Net.Feature.Feature(new Point(new Position(12, 123)));

            Assert.DoesNotThrow(() =>
            {
                equal1 = feature1 == feature2;
                equal2 = feature1.Equals(feature2);
            });

            Assert.IsFalse(equal1);
            Assert.IsFalse(equal2);
        }

        [Test]
        public void Feature_Equals_Other_Geometry_Null_Issue115()
        {
            bool equal1 = false;
            bool equal2 = false;

            var feature1 = new Net.Feature.Feature(new Point(new Position(12, 123)));
            var feature2 = new Net.Feature.Feature(null);

            Assert.DoesNotThrow(() =>
            {
                equal1 = feature1 == feature2;
                equal2 = feature1.Equals(feature2);
            });

            Assert.IsFalse(equal1);
            Assert.IsFalse(equal2);
        }

        [Test]
        public void Feature_Equals_All_Geometry_Null_Issue115()
        {
            bool equal1 = false;
            bool equal2 = false;

            var feature1 = new Net.Feature.Feature(null);
            var feature2 = new Net.Feature.Feature(null);

            Assert.DoesNotThrow(() =>
            {
                equal1 = feature1 == feature2;
                equal2 = feature1.Equals(feature2);
            });

            Assert.IsTrue(equal1);
            Assert.IsTrue(equal2);
        }


        private IGeometryObject GetGeometry()
        {
            var coordinates = new List<LineString>
            {
                new LineString(new List<IPosition>
                {
                    new Position(52.370725881211314, 4.889259338378906),
                    new Position(52.3711451105601, 4.895267486572266),
                    new Position(52.36931095278263, 4.892091751098633),
                    new Position(52.370725881211314, 4.889259338378906)
                }),
                new LineString(new List<IPosition>
                {
                    new Position(52.370725881211314, 4.989259338378906),
                    new Position(52.3711451105601, 4.995267486572266),
                    new Position(52.36931095278263, 4.992091751098633),
                    new Position(52.370725881211314, 4.989259338378906)
                })
            };
            var multiLine = new MultiLineString(coordinates);
            return multiLine;
        }

        public static Dictionary<string, object> GetPropertiesInRandomOrder()
        {
            var properties = new Dictionary<string, object>()
            {
                { "DateTimeProperty",  DateTime.Now },
                { "IntProperty",  -1 },
                { "EnumProperty",  TestFeatureEnum.Value1 },
                { "BooleanProperty", true },
                { "DoubleProperty",  1.2345d },
                { "StringProperty",  "Hello, GeoJSON !" }
            };
            var randomlyOrdered = new Dictionary<string, object>();
            var randomlyOrderedKeys = properties.Keys.Select(k => Guid.NewGuid() + k).OrderBy(k => k).ToList();
            foreach (var key in randomlyOrderedKeys)
            {
                var theKey = key.Substring(36);
                randomlyOrdered.Add(theKey, properties[theKey]);
            }
            return randomlyOrdered;
        }

        private void Assert_Are_Equal(Net.Feature.Feature left, Net.Feature.Feature right)
        {
            Assert.AreEqual(left, right);

            Assert.IsTrue(left.Equals(right));
            Assert.IsTrue(right.Equals(left));

            Assert.IsTrue(left.Equals(left));
            Assert.IsTrue(right.Equals(right));

            Assert.IsTrue(left == right);
            Assert.IsTrue(right == left);

            Assert.IsFalse(left != right);
            Assert.IsFalse(right != left);

            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        private void Assert_Are_Not_Equal(Net.Feature.Feature left, Net.Feature.Feature right)
        {
            Assert.AreNotEqual(left, right);

            Assert.IsFalse(left.Equals(right));
            Assert.IsFalse(right.Equals(left));

            Assert.IsFalse(left == right);
            Assert.IsFalse(right == left);

            Assert.IsTrue(left != right);
            Assert.IsTrue(right != left);

            Assert.AreNotEqual(left.GetHashCode(), right.GetHashCode());
        }
    }
}