using System;
using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Feature;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Legacy;

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

            ClassicAssert.AreEqual(CRSType.Name, crs.Type);
        }

        [Test]
        public void Has_Name_Property_With_Name()
        {
            var name = "EPSG:31370";
            var crs = new NamedCRS(name);

            ClassicAssert.IsTrue(crs.Properties.ContainsKey("name"));
            ClassicAssert.AreEqual(name, crs.Properties["name"]);
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
            ClassicAssert.Throws<ArgumentNullException>(() => { var collection = new FeatureCollection() { CRS = new NamedCRS(null) }; });
        }

        [Test]
        public void Ctor_Throws_ArgumentNullExpection_When_Name_Is_Empty()
        {
            ClassicAssert.Throws<ArgumentException>(() => { var collection = new FeatureCollection() { CRS = new NamedCRS(string.Empty) }; });
        }

        [Test]
        public void Equals_GetHashCode_Contract()
        {
            var name = "EPSG:31370";

            var left = new NamedCRS(name);
            var right = new NamedCRS(name);

            ClassicAssert.AreEqual(left, right);

            ClassicAssert.IsTrue(left == right);
            ClassicAssert.IsTrue(right == left);

            ClassicAssert.IsTrue(left.Equals(right));
            ClassicAssert.IsTrue(right.Equals(left));

            ClassicAssert.IsTrue(left.Equals(left));
            ClassicAssert.IsTrue(right.Equals(right));

            ClassicAssert.AreEqual(left.GetHashCode(), right.GetHashCode());

            name = "EPSG:25832";
            right = new NamedCRS(name);

            ClassicAssert.AreNotEqual(left, right);

            ClassicAssert.IsFalse(left == right);
            ClassicAssert.IsFalse(right == left);

            ClassicAssert.IsFalse(left.Equals(right));
            ClassicAssert.IsFalse(right.Equals(left));

            ClassicAssert.AreNotEqual(left.GetHashCode(), right.GetHashCode());
        }
    }
}