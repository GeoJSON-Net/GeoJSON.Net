using System;
using System.Text.Json;
using GeoJSON.Net.CoordinateReferenceSystem;
using GeoJSON.Net.Geometry;
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
            var collection = new Point(new Position(1, 2, 3)) { CRS = new LinkedCRS(Href) };
            var actualJson = JsonSerializer.Serialize(collection);

            JsonAssert.Contains("{\"properties\":{\"href\":\"http://localhost\"},\"type\":\"link\"}", actualJson);
        }

        [Test]
        public void Can_Deserialize_CRS_issue_101()
        {
            const string pointJson = "{\"type\":\"Point\",\"coordinates\":[2.0,1.0,3.0],\"crs\":{\"properties\":{\"href\":\"http://localhost\"},\"type\":\"link\"}}";
            var pointWithCRS = JsonSerializer.Deserialize<Point>(pointJson);
            var linkCRS = pointWithCRS.CRS as LinkedCRS;

            Assert.IsNotNull(linkCRS);
            Assert.AreEqual(CRSType.Link, linkCRS.Type);
            Assert.AreEqual(Href, linkCRS.Properties["href"]);
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
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

            // Assert that a argument exception is thrown, and that it is for href.
            var argumentExpection = Assert.Throws<ArgumentException>(() => { var crs = new LinkedCRS("http://not-a-valid-<>-url"); });
            Assert.True(argumentExpection.Message.ToLower().Contains("href"));
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

        [Test]
        public void Equals_GetHashCode_Contract()
        {
            var left = new LinkedCRS(Href);
            var right = new LinkedCRS(Href);

            Assert.AreEqual(left, right);

            Assert.IsTrue(left == right);
            Assert.IsTrue(right == left);

            Assert.IsTrue(left.Equals(right));
            Assert.IsTrue(right.Equals(left));

            Assert.IsTrue(left.Equals(left));
            Assert.IsTrue(right.Equals(right));

            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());

            right = new LinkedCRS(Href + "?query=null");

            Assert.AreNotEqual(left, right);

            Assert.IsFalse(left == right);
            Assert.IsFalse(right == left);

            Assert.IsFalse(left.Equals(right));
            Assert.IsFalse(right.Equals(left));

            Assert.AreNotEqual(left.GetHashCode(), right.GetHashCode());
        }
    }
}