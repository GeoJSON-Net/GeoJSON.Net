using System;
using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.CoordinateReferenceSystem
{
    [TestFixture]
    public class LinkedCRSTests : TestBase
    {
        private const string Href = "http://localhost";

        [Test]
        public void LinkedCrs_Has_Correct_Type()
        {
            var crs = new LinkedCRS(Href);
            Assert.AreEqual(CRSType.Link, crs.Type);
        }

        [Test]
        public void LinkedCrs_Has_Href_Property_With_Href()
        {
            var crs = new LinkedCRS(Href);

            Assert.IsTrue(crs.Properties.ContainsKey("href"));
            Assert.AreEqual(Href, crs.Properties["href"]);
        }

        [Test]
        public void LinkedCrs_SerializationWithValue()
        {
            var collection = new Point(new GeographicPosition(1, 2, 3)) { CRS = new LinkedCRS(Href) };
            var actualJson = JsonConvert.SerializeObject(collection);

            JsonAssert.Contains("{\"properties\":{\"href\":\"http://localhost\"},\"type\":\"Link\"}", actualJson);
        }

        [Test]
        public void LinkedCrs_Ctor_Throws_ArgumentNullExpection_When_Href_String_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => { var crs = new LinkedCRS((string)null); });
        }

        [Test]
        public void LinkedCrs_Ctor_Throws_ArgumentNullExpection_When_Href_Uri_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => { var crs = new LinkedCRS((Uri)null); });
        }

        [Test]
        public void LinkedCrs_Ctor_Throws_ArgumentNullExpection_When_Name_Is_Empty()
        {
            Assert.Throws<ArgumentException>(() => { var crs = new LinkedCRS(string.Empty); });
        }
    }
}