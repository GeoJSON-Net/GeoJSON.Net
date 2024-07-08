using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace GeoJSON.Net.Tests.Feature
{
    [TestFixture]
    public class FeatureCollectionTests : TestBase
    {
        [Test]
        public void Ctor_Throws_ArgumentNullException_When_Features_Is_Null()
        {
            ClassicAssert.Throws<ArgumentNullException>(() =>
            {
                var featureCollection = new FeatureCollection(null);
            });
        }

        [Test]
        public void Can_Deserialize()
        {
            string json = GetExpectedJson();

            var featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(json);

            ClassicAssert.IsNotNull(featureCollection);
            ClassicAssert.IsNotNull(featureCollection.Features);
            ClassicAssert.AreEqual(featureCollection.Features.Count, 3);
            ClassicAssert.AreEqual(featureCollection.Features.Count(x => x.Geometry.Type == GeoJSONObjectType.Point), 1);
            ClassicAssert.AreEqual(featureCollection.Features.Count(x => x.Geometry.Type == GeoJSONObjectType.MultiPolygon), 1);
            ClassicAssert.AreEqual(featureCollection.Features.Count(x => x.Geometry.Type == GeoJSONObjectType.Polygon), 1);
        }
        
        [Test]
        public void Can_DeserializeGeneric()
        {
            string json = GetExpectedJson();

            var featureCollection = JsonConvert.DeserializeObject<FeatureCollection<FeatureCollectionTestPropertyObject>>(json);

            ClassicAssert.IsNotNull(featureCollection);
            ClassicAssert.IsNotNull(featureCollection.Features);
            ClassicAssert.AreEqual(featureCollection.Features.Count, 3);
            ClassicAssert.AreEqual("DD", featureCollection.Features.First().Properties.Name);
            ClassicAssert.AreEqual(123, featureCollection.Features.First().Properties.Size);
        }
        
        

        [Test]
        public void FeatureCollectionSerialization()
        {
            var model = new FeatureCollection();
            for (var i = 10; i-- > 0;)
            {
                var geom = new LineString(new[]
                {
                    new Position(51.010, -1.034),
                    new Position(51.010, -0.034)
                });

                var props = new Dictionary<string, object>
                {
                    { "test1", "1" },
                    { "test2", 2 }
                };

                var feature = new Net.Feature.Feature(geom, props);
                model.Features.Add(feature);
            }

            var actualJson = JsonConvert.SerializeObject(model);

            ClassicAssert.IsNotNull(actualJson);

            ClassicAssert.IsFalse(string.IsNullOrEmpty(actualJson));
        }
        
        [Test]
        public void FeatureCollection_Equals_GetHashCode_Contract()
        {
            var left = GetFeatureCollection();
            var right = GetFeatureCollection();

            Assert_Are_Equal(left, right);
        }

        [Test]
        public void Serialized_And_Deserialized_FeatureCollection_Equals_And_Share_HashCode()
        {
            var leftFc = GetFeatureCollection();
            var leftJson = JsonConvert.SerializeObject(leftFc);
            var left = JsonConvert.DeserializeObject<FeatureCollection>(leftJson);

            var rightFc = GetFeatureCollection();
            var rightJson = JsonConvert.SerializeObject(rightFc);
            var right = JsonConvert.DeserializeObject<FeatureCollection>(rightJson);

            Assert_Are_Equal(left, right);
        }

        [Test]
        public void FeatureCollection_Test_IndexOf()
        {
            var model = new FeatureCollection();
            var expectedIds = new List<string>();
            var expectedIndexes = new List<int>();

            for (var i = 0; i < 10; i++)
            {
                var id = "id" + i;

                expectedIds.Add(id);
                expectedIndexes.Add(i);

                var geom = new LineString(new[]
                {
                    new Position(51.010, -1.034),
                    new Position(51.010, -0.034)
                });

                var props = FeatureTests.GetPropertiesInRandomOrder();

                var feature = new Net.Feature.Feature(geom, props, id);
                model.Features.Add(feature);
            }

            for (var i = 0; i < 10; i++)
            {
                var actualFeature = model.Features[i];
                var actualId = actualFeature.Id;
                var actualIndex = model.Features.IndexOf(actualFeature);

                var expectedId = expectedIds[i];
                var expectedIndex = expectedIndexes[i];

                ClassicAssert.AreEqual(expectedId, actualId);
                ClassicAssert.AreEqual(expectedIndex, actualIndex);

                ClassicAssert.Inconclusive("not supported. the Feature.Id is optional. " + 
                    " create a new class that inherits from" +
                    " Feature and then override Equals and GetHashCode");

            }

        }


        private FeatureCollection GetFeatureCollection()
        {
            var model = new FeatureCollection();
            for (var i = 10; i-- > 0;)
            {
                var geom = new LineString(new[]
                {
                    new Position(51.010, -1.034),
                    new Position(51.010, -0.034)
                });

                var props = FeatureTests.GetPropertiesInRandomOrder();

                var feature = new Net.Feature.Feature(geom, props);
                model.Features.Add(feature);
            }
            return model;
        }

        private void Assert_Are_Equal(FeatureCollection left, FeatureCollection right)
        {
            ClassicAssert.AreEqual(left, right);

            ClassicAssert.IsTrue(left.Equals(right));
            ClassicAssert.IsTrue(right.Equals(left));

            ClassicAssert.IsTrue(left.Equals(left));
            ClassicAssert.IsTrue(right.Equals(right));

            ClassicAssert.IsTrue(left == right);
            ClassicAssert.IsTrue(right == left);

            ClassicAssert.IsFalse(left != right);
            ClassicAssert.IsFalse(right != left);

            ClassicAssert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }
    }
    
    
    internal class FeatureCollectionTestPropertyObject {
        public string Name { get; set; }
        public int Size { get; set; }
    }
}