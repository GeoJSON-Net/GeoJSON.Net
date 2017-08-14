using System;
using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Features;
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

            Assert.AreEqual(CRSType.Name, crs.Type);
        }

        [Test]
        public void Has_Name_Property_With_Name()
        {
            var name = "EPSG:31370";
            var crs = new NamedCRS(name);

            Assert.IsTrue(crs.Properties.ContainsKey("name"));
            Assert.AreEqual(name, crs.Properties["name"]);
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

            Assert.AreEqual(left, right);

            Assert.IsTrue(left == right);
            Assert.IsTrue(right == left);

            Assert.IsTrue(left.Equals(right));
            Assert.IsTrue(right.Equals(left));

            Assert.IsTrue(left.Equals(left));
            Assert.IsTrue(right.Equals(right));

            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());

            name = "EPSG:25832";
            right = new NamedCRS(name);

            Assert.AreNotEqual(left, right);

            Assert.IsFalse(left == right);
            Assert.IsFalse(right == left);

            Assert.IsFalse(left.Equals(right));
            Assert.IsFalse(right.Equals(left));

            Assert.AreNotEqual(left.GetHashCode(), right.GetHashCode());
        }
    }
}