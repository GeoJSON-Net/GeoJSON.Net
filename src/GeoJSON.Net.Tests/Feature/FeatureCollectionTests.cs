using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Feature
{
    [TestFixture]
    public class FeatureCollectionTests : TestBase
    {
        [Test]
        public void Ctor_Throws_ArgumentNullException_When_Features_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var featureCollection = new FeatureCollection(null);
            });
        }

        [Test]
        public void Can_Deserialize()
        {
            string json = GetExpectedJson();

            var featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(json);

            Assert.That(featureCollection, Is.Not.Null);
            Assert.That(featureCollection.Features, Is.Not.Null);
            Assert.That(featureCollection.Features.Count, Is.EqualTo(3));
            Assert.That(featureCollection.Features.Count(x => x.Geometry.Type == GeoJSONObjectType.Point), Is.EqualTo(1));
            Assert.That(featureCollection.Features.Count(x => x.Geometry.Type == GeoJSONObjectType.MultiPolygon), Is.EqualTo(1));
            Assert.That(featureCollection.Features.Count(x => x.Geometry.Type == GeoJSONObjectType.Polygon), Is.EqualTo(1));
        }
        
        [Test]
        public void Can_DeserializeGeneric()
        {
            string json = GetExpectedJson();

            var featureCollection = JsonConvert.DeserializeObject<FeatureCollection<FeatureCollectionTestPropertyObject>>(json);

            Assert.That(featureCollection, Is.Not.Null);
            Assert.That(featureCollection.Features, Is.Not.Null);
            Assert.That(featureCollection.Features.Count, Is.EqualTo(3));
            Assert.That(featureCollection.Features.First().Properties.Name, Is.EqualTo("DD"));
            Assert.That(featureCollection.Features.First().Properties.Size, Is.EqualTo(123));
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

            Assert.That(actualJson, Is.Not.Null);

            Assert.That(string.IsNullOrEmpty(actualJson), Is.False);
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

                Assert.That(actualId, Is.EqualTo(expectedId));
                Assert.That(actualIndex, Is.EqualTo(expectedIndex));

                Assert.Inconclusive("not supported. the Feature.Id is optional. " + 
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
    }
    
    
    internal class FeatureCollectionTestPropertyObject {
        public string Name { get; set; }
        public int Size { get; set; }
    }
}