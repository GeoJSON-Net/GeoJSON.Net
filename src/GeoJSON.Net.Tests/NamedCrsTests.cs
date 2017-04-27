using System;
using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Feature;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests
{
    [TestFixture]
    public class NamedCrsTests : TestBase
    {
        [Test]
        public void NamedCrsHasCorrectType()
        {
            var collection = new FeatureCollection(null) { CRS = new NamedCRS("EPSG:31370") };
            Assert.AreEqual(CRSType.Name, collection.CRS.Type);
        }

        [Test]
        public void NamedCrsSerializationWithValue()
        {
            var collection = new FeatureCollection(null) { CRS = new NamedCRS("EPSG:31370") };
            var serializedData = JsonConvert.SerializeObject(collection);
            Assert.IsTrue(serializedData.Contains("\"crs\":{\"type\":\"Name\",\"properties\":{\"name\":\"EPSG:31370\"}}"));
        }

        [Test]
        public void NamedCrsSerializationNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FeatureCollection(null) {CRS = new NamedCRS(null)});
        }

        [Test]
        public void NamedCrsSerializationEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new FeatureCollection(null) { CRS = new NamedCRS("") });
        }

        [Test]
        public void NamedCrsSerializationNotSet()
        {
            var collection = new FeatureCollection(null);

            var serializedData = JsonConvert.SerializeObject(collection, DefaultJsonSerializerSettings);
            Assert.IsTrue(!serializedData.Contains("\"crs\""));
        }
    }
}