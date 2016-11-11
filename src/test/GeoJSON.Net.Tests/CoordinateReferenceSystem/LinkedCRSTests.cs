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
        public void Has_Correct_Type()
        {
            var crs = new LinkedCRS(Href);
            Assert.AreEqual(CRSType.Link, crs.Type);
        }

        [Test]
        public void Has_Href_Property_With_Href()
        {
            var crs = new LinkedCRS(Href);

            Assert.IsTrue(crs.Properties.ContainsKey("href"));
            Assert.AreEqual(Href, crs.Properties["href"]);
        }

        [Test]
        public void Has_Type_Property()
        {
            const string type = "ogcwkt";
            var crs = new LinkedCRS(Href, type);

            Assert.IsTrue(crs.Properties.ContainsKey("type"));
            Assert.AreEqual(type, crs.Properties["type"]);
        }

        [Test]
        public void Can_Serialize()
        {
            var collection = new Point(new GeographicPosition(1, 2, 3)) { CRS = new LinkedCRS(Href) };
            var actualJson = JsonConvert.SerializeObject(collection);

            JsonAssert.Contains("{\"properties\":{\"href\":\"http://localhost\"},\"type\":\"link\"}", actualJson);
        }

        [Test]
        public void Ctor_Throws_ArgumentNullExpection_When_Href_String_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => { var crs = new LinkedCRS((string)null); });
        }

        [Test]
        public void Ctor_Throws_ArgumentNullExpection_When_Href_Uri_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => { var crs = new LinkedCRS((Uri)null); });
        }

        [Test]
        public void Ctor_Throws_ArgumentExpection_When_Href_Is_Not_Dereferencable_Uri()
        {
            var argumentExpection = Assert.Throws<ArgumentException>(() => { var crs = new LinkedCRS("http://not-a-valid-<>-url"); });

            Assert.AreEqual("must be a dereferenceable URI\r\nParameter name: href", argumentExpection.Message);
        }

        [Test]
        public void Ctor_Does_Not_Throw_When_Href_Is_Dereferencable_Uri()
        {
            Assert.DoesNotThrow(() => { var crs = new LinkedCRS("data.crs"); });
        }

        [Test]
        public void Ctor_Throws_ArgumentNullExpection_When_Name_Is_Empty()
        {
            Assert.Throws<ArgumentException>(() => { var crs = new LinkedCRS(string.Empty); });
        }
    }
}