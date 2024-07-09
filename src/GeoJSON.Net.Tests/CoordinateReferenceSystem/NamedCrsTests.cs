using System;
using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Feature;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.CoordinateReferenceSystem
{
    [TestFixture]
    public class NamedCRSTests : TestBase
    {
        [Test]
        public void Has_Correct_Type()
        {
            var name = "EPSG:31370";
            var crs = new NamedCRS(name);

            Assert.That(crs.Type, Is.EqualTo(CRSType.Name));
        }

        [Test]
        public void Has_Name_Property_With_Name()
        {
            var name = "EPSG:31370";
            var crs = new NamedCRS(name);

            Assert.That(crs.Properties.ContainsKey("name"));
            Assert.That(crs.Properties["name"], Is.EqualTo(name));
        }

        [Test]
        public void Can_Serialize()
        {
            var collection = new FeatureCollection() { CRS = new NamedCRS("EPSG:31370") };
            var actualJson = JsonConvert.SerializeObject(collection);

            JsonAssert.Contains("{\"properties\":{\"name\":\"EPSG:31370\"},\"type\":\"name\"}", actualJson);
        }

        [Test]
        public void Ctor_Throws_ArgumentNullExpection_When_Name_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => { var collection = new FeatureCollection() { CRS = new NamedCRS(null) }; });
        }

        [Test]
        public void Ctor_Throws_ArgumentNullExpection_When_Name_Is_Empty()
        {
            Assert.Throws<ArgumentException>(() => { var collection = new FeatureCollection() { CRS = new NamedCRS(string.Empty) }; });
        }

        [Test]
        public void Equals_GetHashCode_Contract()
        {
            var name = "EPSG:31370";

            var left = new NamedCRS(name);
            var right = new NamedCRS(name);

            Assert.That(right, Is.EqualTo(left));

            Assert.That(left == right);
            Assert.That(right == left);

            Assert.That(left.Equals(right));
            Assert.That(right.Equals(left));

            Assert.That(left.Equals(left));
            Assert.That(right.Equals(right));

            Assert.That(right.GetHashCode(), Is.EqualTo(left.GetHashCode()));

            name = "EPSG:25832";
            right = new NamedCRS(name);

            Assert.That(right, Is.Not.EqualTo(left));

            Assert.That(left == right, Is.False);
            Assert.That(right == left, Is.False);

            Assert.That(left.Equals(right), Is.False);
            Assert.That(right.Equals(left), Is.False);

            Assert.That(right.GetHashCode(), Is.Not.EqualTo(left.GetHashCode()));
        }
    }
}