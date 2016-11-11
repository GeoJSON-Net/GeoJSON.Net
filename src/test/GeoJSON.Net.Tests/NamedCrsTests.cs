using System;
using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Feature;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace GeoJSON.Net.Tests
{
    [TestClass]
    public class NamedCrsTests : TestBase
    {
        [TestMethod]
        public void NamedCrsHasCorrectType()
        {
            var collection = new FeatureCollection(null) { CRS = new NamedCRS("EPSG:31370") };
            Assert.AreEqual(CRSType.Name, collection.CRS.Type);
        }

        [TestMethod]
        public void NamedCrsSerializationWithValue()
        {
            var collection = new FeatureCollection(null) { CRS = new NamedCRS("EPSG:31370") };
            var serializedData = JsonConvert.SerializeObject(collection);
            Assert.IsTrue(serializedData.Contains("\"crs\":{\"type\":\"Name\",\"properties\":{\"name\":\"EPSG:31370\"}}"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NamedCrsSerializationNull()
        {
            var collection = new FeatureCollection(null) { CRS = new NamedCRS(null) };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NamedCrsSerializationEmpty()
        {
            var collection = new FeatureCollection(null) { CRS = new NamedCRS("") };
        }

        [TestMethod]
        public void NamedCrsSerializationNotSet()
        {
            var collection = new FeatureCollection(null);

            var serializedData = JsonConvert.SerializeObject(collection, DefaultJsonSerializerSettings);
            Assert.IsTrue(!serializedData.Contains("\"crs\""));
        }
    }
}