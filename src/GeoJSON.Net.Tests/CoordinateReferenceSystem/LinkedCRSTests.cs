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
            Assert.That(crs.Type, Is.EqualTo(CRSType.Link));
        }

        [Test]
        public void Has_Href_Property_With_Href()
        {
            var crs = new LinkedCRS(Href);

            Assert.That(crs.Properties.ContainsKey("href"));
            Assert.That(crs.Properties["href"], Is.EqualTo(Href));
        }

        [Test]
        public void Has_Type_Property()
        {
            const string type = "ogcwkt";
            var crs = new LinkedCRS(Href, type);

            Assert.That(crs.Properties.ContainsKey("type"));
            Assert.That(crs.Properties["type"], Is.EqualTo(type));
        }

        [Test]
        public void Can_Serialize()
        {
            var collection = new Point(new Position(1, 2, 3)) { CRS = new LinkedCRS(Href) };
            var actualJson = JsonConvert.SerializeObject(collection);

            JsonAssert.Contains("{\"properties\":{\"href\":\"http://localhost\"},\"type\":\"link\"}", actualJson);
        }

        [Test]
        public void Can_Deserialize_CRS_issue_101()
        {
            const string pointJson = "{\"type\":\"Point\",\"coordinates\":[2.0,1.0,3.0],\"crs\":{\"properties\":{\"href\":\"http://localhost\"},\"type\":\"link\"}}";
            var pointWithCRS = JsonConvert.DeserializeObject<Point>(pointJson);
            var linkCRS = pointWithCRS.CRS as LinkedCRS;

            Assert.That(linkCRS, Is.Not.Null);
            Assert.That(linkCRS.Type, Is.EqualTo(CRSType.Link));
            Assert.That(linkCRS.Properties["href"], Is.EqualTo(Href));
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
#if NETFRAMEWORK
            var expected = $"must be a dereferenceable URI{Environment.NewLine}Parameter name: href";
#else
            var expected = $"must be a dereferenceable URI (Parameter 'href')";
#endif
#if NETCOREAPP1_1
            System.Globalization.CultureInfo.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
#else
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
#endif

            var argumentExpection = Assert.Throws<ArgumentException>(() => { var crs = new LinkedCRS("http://not-a-valid-<>-url"); });
            Assert.That(argumentExpection.Message, Is.EqualTo(expected));
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

            Assert.That(right, Is.EqualTo(left));

            Assert.That(left == right);
            Assert.That(right == left);

            Assert.That(left.Equals(right));
            Assert.That(right.Equals(left));

            Assert.That(left.Equals(left));
            Assert.That(right.Equals(right));

            Assert.That(right.GetHashCode(), Is.EqualTo(left.GetHashCode()));

            right = new LinkedCRS(Href + "?query=null");

            Assert.That(right, Is.Not.EqualTo(left));

            Assert.That(left == right, Is.False);
            Assert.That(right == left, Is.False);

            Assert.That(left.Equals(right), Is.False);
            Assert.That(right.Equals(left), Is.False);

            Assert.That(right.GetHashCode(), Is.Not.EqualTo(left.GetHashCode()));
        }
    }
}