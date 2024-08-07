using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

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

            Assert.That(feature, Is.Not.Null);
            Assert.That(feature.Properties, Is.Not.Null);
            Assert.That(feature.Properties.Any());

            Assert.That(feature.Properties.ContainsKey("name"));
            Assert.That(feature.Properties["name"], Is.EqualTo("Dinagat Islands"));

            Assert.That(feature.Id, Is.EqualTo("test-id"));

            Assert.That(feature.Geometry.Type, Is.EqualTo(GeoJSONObjectType.Point));
        }

        [Test]
        public void Can_Deserialize_Feature_Without_Props()
        {
            var json = GetExpectedJson();

            var feature = JsonConvert.DeserializeObject<Net.Feature.Feature>(json);

            Assert.That(feature, Is.Not.Null);
            Assert.That(feature.Properties, Is.Not.Null);
            Assert.That(feature.Properties, Is.Empty);

            Assert.That(feature.Geometry.Type, Is.EqualTo(GeoJSONObjectType.Polygon));
        }

        [Test]
        public void Can_Deserialize_Feature_With_Null_Geometry()
        {
            var json = GetExpectedJson();

            var feature = JsonConvert.DeserializeObject<Net.Feature.Feature>(json);

            Assert.That(feature, Is.Not.Null);
            Assert.That(feature.Properties, Is.Not.Null);
            Assert.That(feature.Properties.Any());
            Assert.That(feature.Properties.ContainsKey("name"));
            Assert.That(feature.Properties["name"], Is.EqualTo("Unlocalized Feature"));

            Assert.That(feature.Geometry, Is.Null);
        }

        [Test]
        public void Can_Deserialize_Feature_Without_Geometry()
        {
            var json = GetExpectedJson();

            var feature = JsonConvert.DeserializeObject<Net.Feature.Feature>(json);

            Assert.That(feature, Is.Not.Null);
            Assert.That(feature.Properties, Is.Not.Null);
            Assert.That(feature.Properties.Any());
            Assert.That(feature.Properties.ContainsKey("name"));
            Assert.That(feature.Properties["name"], Is.EqualTo("Unlocalized Feature"));

            Assert.That(feature.Geometry, Is.Null);
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
        public void Can_Serialize_Dictionary_Subclass()
        {
            var properties =
                new TestFeaturePropertyDictionary()
                {
                     BooleanProperty = true,
                     DoubleProperty = 1.2345d,
                     EnumProperty = TestFeatureEnum.Value1,
                     IntProperty = -1,
                     StringProperty = "Hello, GeoJSON !"
                };

            Net.Feature.Feature feature = new Net.Feature.Feature(new Point(new Position(10, 10)), properties);

            var expectedJson = this.GetExpectedJson();
            var actualJson = JsonConvert.SerializeObject(feature);

            Assert.That(string.IsNullOrEmpty(expectedJson), Is.False);
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

            Assert.That(feature.Properties, Is.Not.Null);
            Assert.That(feature.Properties.Count > 1);
            Assert.That(feature.Properties.Count, Is.EqualTo(6));
        }

        [Test]
        public void Ctor_Can_Add_Properties_Using_Object_Inheriting_Dictionary()
        {
            int expectedProperties = 6;

            var properties = new TestFeaturePropertyDictionary()
            {
                BooleanProperty = true,
                DateTimeProperty = DateTime.Now,
                DoubleProperty = 1.2345d,
                EnumProperty = TestFeatureEnum.Value1,
                IntProperty = -1,
                StringProperty = "Hello, GeoJSON !"
            };

            Net.Feature.Feature feature = new Net.Feature.Feature(new Point(new Position(10, 10)), properties);

            Assert.That(feature.Properties, Is.Not.Null);
            Assert.That(feature.Properties.Count > 1);
            Assert.That(
                expectedProperties, Is.EqualTo(feature.Properties.Count),
                $"Expected: {expectedProperties} Actual: {feature.Properties.Count}");
        }

        [Test]
        public void Ctor_Creates_Properties_Collection_When_Passed_Null_Proper_Object()
        {
            Net.Feature.Feature feature = new Net.Feature.Feature(new Point(new Position(10, 10)), (object)null);

            Assert.That(feature.Properties, Is.Not.Null);
            Assert.That(feature.Properties, Is.Empty);
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

            Assert.That(equal1, Is.False);
            Assert.That(equal2, Is.False);
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

            Assert.That(equal1, Is.False);
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

            Assert.That(equal1);
            Assert.That(equal2);
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

            Assert.That(equal1, Is.False);
            Assert.That(equal2, Is.False);
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

            Assert.That(equal1, Is.False);
            Assert.That(equal2, Is.False);
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

            Assert.That(equal1);
            Assert.That(equal2);
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

        public static IDictionary<string, object> GetPropertiesInRandomOrder()
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
            Assert.That(right, Is.EqualTo(left));

            Assert.That(left.Equals(right));
            Assert.That(right.Equals(left));

            Assert.That(left.Equals(left));
            Assert.That(right.Equals(right));

            Assert.That(left == right);
            Assert.That(right == left);

            Assert.That(left != right, Is.False);
            Assert.That(right != left, Is.False);

            Assert.That(right.GetHashCode(), Is.EqualTo(left.GetHashCode()));
        }

        private void Assert_Are_Not_Equal(Net.Feature.Feature left, Net.Feature.Feature right)
        {
            Assert.That(right, Is.Not.EqualTo(left));

            Assert.That(left.Equals(right), Is.False);
            Assert.That(right.Equals(left), Is.False);

            Assert.That(left == right, Is.False);
            Assert.That(right == left, Is.False);

            Assert.That(left != right);
            Assert.That(right != left);

            Assert.That(right.GetHashCode(), Is.Not.EqualTo(left.GetHashCode()));
        }
    }
}