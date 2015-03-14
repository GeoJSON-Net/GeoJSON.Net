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
        public void NamedCrs_Has_Correct_Type()
        {
            var name = "EPSG:31370";
            var crs = new NamedCRS(name);

            Assert.AreEqual(CRSType.Name, crs.Type);
        }

        [Test]
        public void NamedCrs_Has_Name_Property_With_Name()
        {
            var name = "EPSG:31370";
            var crs = new NamedCRS(name);

            Assert.IsTrue(crs.Properties.ContainsKey("name"));
            Assert.AreEqual(name, crs.Properties["name"]);
        }

        [Test]
        public void NamedCrsSerializationWithValue()
        {
            var collection = new FeatureCollection() { CRS = new NamedCRS("EPSG:31370") };
            var actualJson = JsonConvert.SerializeObject(collection);

            JsonAssert.Contains("{\"properties\":{\"name\":\"EPSG:31370\"},\"type\":\"Name\"}", actualJson);
        }

        [Test]
        public void NamedCrs_Ctor_Throws_ArgumentNullExpection_When_Name_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => { var collection = new FeatureCollection() { CRS = new NamedCRS(null) }; });
        }

        [Test]
        public void NamedCrs_Ctor_Throws_ArgumentNullExpection_When_Name_Is_Empty()
        {
            Assert.Throws<ArgumentException>(() => { var collection = new FeatureCollection() { CRS = new NamedCRS(string.Empty) }; });
        }

        [Test]
        public void NamedCrsSerializationNotSet()
        {
            var collection = new FeatureCollection();

            var serializedData = JsonConvert.SerializeObject(collection);
            Assert.IsTrue(!serializedData.Contains("\"crs\""));
        }
    }
}